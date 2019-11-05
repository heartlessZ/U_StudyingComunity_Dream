
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using U_StudyingCommunity_Dream.Articles;
using U_StudyingCommunity_Dream.Articles.Dtos;
using U_StudyingCommunity_Dream.UserDetails;
using U_StudyingCommunity_Dream.Enums;

namespace U_StudyingCommunity_Dream.Articles
{
    /// <summary>
    /// Article应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ArticleAppService : U_StudyingCommunity_DreamAppServiceBase, IArticleAppService
    {
        private readonly IRepository<Article, long> _entityRepository;
        private readonly IRepository<Article_ArticleCategory, long> _articleTagsRepository;
        private readonly IRepository<ArticleCategory, int> _categoryRepository;
        private readonly IRepository<Comment, long> _commentRepository;
        private readonly IRepository<UserDetail, Guid> _userDetailRepository;



        /// <summary>
        /// 构造函数 
        ///</summary>
        public ArticleAppService(
        IRepository<Article, long> entityRepository
        , IRepository<Article_ArticleCategory, long> articleTagsRepository
        , IRepository<UserDetail, Guid> userDetailRepository
        , IRepository<Comment, long> commentRepository
        , IRepository<ArticleCategory, int> categoryRepository
        )
        {
            _entityRepository = entityRepository;
            _articleTagsRepository = articleTagsRepository;
            _userDetailRepository = userDetailRepository;
            _commentRepository = commentRepository;
            _categoryRepository = categoryRepository;
        }


        /// <summary>
        /// 获取Article的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[AbpAllowAnonymous]
        public async Task<PagedResultDto<ArticleListDto>> GetPaged(GetArticlesInput input)
		{
            var articleIds = new List<long>();
            if (input.CategoryId.HasValue)
            {
                articleIds = await _articleTagsRepository.GetAll()
                    .Where(a => a.ArticleCategoryId == input.CategoryId.Value)
                    .Select(i => i.ArticleId)
                    .Distinct()
                    .ToListAsync();
            }
            var query = _entityRepository.GetAll()
                .Where(a => a.ReleaseStatus == input.ReleaseStatus)
                .WhereIf(input.UserDetailId.HasValue, a=>a.UserDetailId == input.UserDetailId.Value)
                .WhereIf(input.CategoryId.HasValue, a=>articleIds.Contains(a.Id));
            
			// TODO:根据传入的参数添加过滤条件
			var count = await query.CountAsync();

			var entityList = await query
					.OrderByDescending(i=>i.CreationTime).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			 var entityListDtos = ObjectMapper.Map<List<ArticleListDto>>(entityList);
            foreach (var article in entityListDtos)
            {
                var userDetail = await _userDetailRepository.GetAsync(article.UserDetailId);
                article.UserName = userDetail.Name;
                article.HeadPortraitUrl = userDetail.HeadPortraitUrl;
                article.CommentCount = await _commentRepository.GetAll().Where(i => i.ArticleId == article.Id && i.Parent == 0).CountAsync();
                var categoryIds = _articleTagsRepository.GetAll().Where(a => a.ArticleId == article.Id).Select(i => i.ArticleCategoryId);
                article.TagNames = string.Join('、', _categoryRepository.GetAll().Where(c => categoryIds.Contains(c.Id)).Select(c => c.Name));
            }

			//var entityListDtos =entityList.MapTo<List<ArticleListDto>>();

			return new PagedResultDto<ArticleListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取ArticleListDto信息
		/// </summary>
		[AbpAllowAnonymous]
		public async Task<ArticleListDto> GetById(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    var result = ObjectMapper.Map<ArticleListDto>(entity);
            if (result != null)
            {
                var user = await _userDetailRepository.GetAsync(result.UserDetailId);
                if (user != null)
                {
                    result.UserName = user.Name;
                    result.HeadPortraitUrl = user.HeadPortraitUrl;
                }
                var categoryIds = _articleTagsRepository.GetAll().Where(i => i.ArticleId == input.Id).Select(i => i.ArticleCategoryId).Distinct();
                if (categoryIds.Count() > 0)
                {
                    result.CategoryIds = categoryIds.ToArray();
                }
            }
            return result;
		}

		/// <summary>
		/// 获取编辑 Article
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetArticleForEditOutput> GetForEdit(NullableIdDto<long> input)
		{
			var output = new GetArticleForEditOutput();
ArticleEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<ArticleEditDto>();

				//articleEditDto = ObjectMapper.Map<List<articleEditDto>>(entity);
			}
			else
			{
				editDto = new ArticleEditDto();
			}

			output.Article = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Article的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateArticleInput input)
		{

			if (input.Article.Id.HasValue)
			{
				await Update(input.Article);
			}
			else
			{
				await Create(input.Article);
			}
		}


		/// <summary>
		/// 新增Article
		/// </summary>
		
		protected virtual async Task<ArticleEditDto> Create(ArticleEditDto input)
		{
            //TODO:新增前的逻辑判断，是否允许新增
            
             var entity = ObjectMapper.Map<Article>(input);
            //var entity=input.MapTo<Article>();
			entity = await _entityRepository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            if (input.CategoryIds.Length > 0)
            {
                await UpdateArticleTags(input.CategoryIds, entity);
            }
            return input;
		}

        /// <summary>
        /// 更新中间表
        /// </summary>
        /// <returns></returns>
        private async Task UpdateArticleTags(int[] tagIds, Article article)
        {
            var sourceIds = _articleTagsRepository.GetAll().Where(a => a.ArticleId == article.Id).Select(i=>i.Id);
            if (sourceIds.Count() > 0)
            {
                await _articleTagsRepository.DeleteAsync(a => sourceIds.Contains(a.Id));
            }
            foreach (var tagId in tagIds)
            {
                await _articleTagsRepository.InsertAsync(new Article_ArticleCategory()
                {
                    ArticleId = article.Id,
                    ArticleCategoryId = tagId
                });
            }
        }

		/// <summary>
		/// 编辑Article
		/// </summary>
		
		protected virtual async Task Update(ArticleEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
            //input.MapTo(entity);

            if (input.CategoryIds.Length > 0)
            {
                await UpdateArticleTags(input.CategoryIds, entity);
            }

            ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除Article信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Article的方法
		/// </summary>
		
		public async Task BatchDelete(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        public async Task<bool> AduitArticleStatus(AuditArticleStatus audit)
        {
            var article = await _entityRepository.GetAsync(audit.ArticleId);
            if (article != null)
            {
                article.ReleaseStatus = audit.Status;
                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        [AbpAllowAnonymous]
        public async Task CreateVisitVolume(EntityDto<long> input)
        {
            var article = await _entityRepository.GetAsync(input.Id);
            article.VisitVolume++;
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CreatePraise(EntityDto<long> input)
        {
            var article = await _entityRepository.GetAsync(input.Id);
            article.Praise++;
            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 导出Article为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

    }
}


