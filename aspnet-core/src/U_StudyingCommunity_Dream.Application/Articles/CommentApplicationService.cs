
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

namespace U_StudyingCommunity_Dream.Articles
{
    /// <summary>
    /// Comment应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class CommentAppService : U_StudyingCommunity_DreamAppServiceBase, ICommentAppService
    {
        private readonly IRepository<Comment, long> _entityRepository;
        private readonly IRepository<UserDetail, Guid> _userDetailRepository;



        /// <summary>
        /// 构造函数 
        ///</summary>
        public CommentAppService(
        IRepository<Comment, long> entityRepository,
        IRepository<UserDetail, Guid> userDetailRepository
        )
        {
            _entityRepository = entityRepository;
            _userDetailRepository = userDetailRepository;
        }


        /// <summary>
        /// 获取Comment的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<CommentListDto>> GetPaged(GetCommentsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<CommentListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<CommentListDto>>();

			return new PagedResultDto<CommentListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取CommentListDto信息
		/// </summary>
		 
		public async Task<CommentListDto> GetById(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<CommentListDto>();
		}

		/// <summary>
		/// 获取编辑 Comment
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetCommentForEditOutput> GetForEdit(NullableIdDto<long> input)
		{
			var output = new GetCommentForEditOutput();
CommentEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<CommentEditDto>();

				//commentEditDto = ObjectMapper.Map<List<commentEditDto>>(entity);
			}
			else
			{
				editDto = new CommentEditDto();
			}

			output.Comment = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Comment的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateCommentInput input)
		{

			if (input.Comment.Id.HasValue)
			{
				await Update(input.Comment);
			}
			else
			{
				await Create(input.Comment);
			}
		}


		/// <summary>
		/// 新增Comment
		/// </summary>
		
		protected virtual async Task<CommentEditDto> Create(CommentEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

             var entity = ObjectMapper.Map <Comment>(input);
            //var entity=input.MapTo<Comment>();
			

			entity = await _entityRepository.InsertAsync(entity);
            return input;
		}

		/// <summary>
		/// 编辑Comment
		/// </summary>
		
		protected virtual async Task Update(CommentEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除Comment信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Comment的方法
		/// </summary>
		
		public async Task BatchDelete(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        /// <summary>
        /// 通过文章Id获取评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<MyPageResultDto<CommentsTreeDto>> GetCommentsByArticleId(GetCommentsInput input)
        {
            var result = new List<CommentsTreeDto>();
            var query = _entityRepository.GetAll()
                .Where(c => c.Parent == 0)
                .Where(c => c.ArticleId == input.ArticleId);
            
            var count = await query.CountAsync();

            var comments = await query
                    .OrderByDescending(i=>i.CreationTime).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();
            foreach (var comment in comments)
            {
                result.Add(new CommentsTreeDto()
                {
                    UserDetailId = comment.UserDetailId,
                    Id = comment.Id,
                    Content = comment.Content
                });
            }

            foreach (var item in result)
            {
                var user = await _userDetailRepository.GetAsync(item.UserDetailId);
                item.Author = user.Surname;
                item.Avatar = user.HeadPortraitUrl;
                item.Children = GetChildrenComments(input.ArticleId, item.Id);
            }
            

            return new MyPageResultDto<CommentsTreeDto>(count, result);
        }

        private List<CommentsTreeDto> GetChildrenComments(long articleId, long parent)
        {
            var result = new List<CommentsTreeDto>();
            var comments = _entityRepository.GetAll()
                .Where(c => c.ArticleId == articleId)
                .Where(c => c.Parent == parent);
            foreach (var comment in comments)
            {
                var user = _userDetailRepository.Get(comment.UserDetailId);
                var entity = new CommentsTreeDto();
                entity.Id = comment.Id;
                entity.UserDetailId = comment.UserDetailId;
                entity.Content = comment.Content;
                entity.Author = user.Surname;
                entity.Avatar = user.HeadPortraitUrl;
                entity.Children = GetChildrenComments(articleId, comment.Id);
                result.Add(entity);
            }
            return result;
        }

        private async Task<List<CommentsTreeDto>> GetChildrenCommentsAsync(long articleId,long parent)
        {
            var result = new List<CommentsTreeDto>();
            var comments = _entityRepository.GetAll()
                .Where(c=>c.ArticleId == articleId)
                .Where(c => c.Parent == parent);
            foreach (var comment in comments)
            {
                var user = await _userDetailRepository.GetAsync(comment.UserDetailId);
                var entity = new CommentsTreeDto();
                entity.Id = comment.Id;
                entity.UserDetailId = comment.UserDetailId;
                entity.Content = comment.Content;
                entity.Author = user.Surname;
                entity.Avatar = user.HeadPortraitUrl;
                entity.Children = await GetChildrenCommentsAsync(articleId, comment.Id);
                result.Add(entity);
            }
            return result;
        }

        /// <summary>
        /// 导出Comment为excel表,等待开发。
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


