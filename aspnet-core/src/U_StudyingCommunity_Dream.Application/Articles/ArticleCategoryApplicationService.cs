
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




namespace U_StudyingCommunity_Dream.Articles
{
    /// <summary>
    /// ArticleCategory应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ArticleCategoryAppService : U_StudyingCommunity_DreamAppServiceBase, IArticleCategoryAppService
    {
        private readonly IRepository<ArticleCategory, int> _entityRepository;

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ArticleCategoryAppService(
        IRepository<ArticleCategory, int> entityRepository
        
        )
        {
            _entityRepository = entityRepository; 
            
        }


        /// <summary>
        /// 获取ArticleCategory的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<ArticleCategoryListDto>> GetPaged(GetArticleCategorysInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<ArticleCategoryListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<ArticleCategoryListDto>>();

			return new PagedResultDto<ArticleCategoryListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取ArticleCategoryListDto信息
		/// </summary>
		 
		public async Task<ArticleCategoryListDto> GetById(EntityDto<int> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<ArticleCategoryListDto>();
		}

		/// <summary>
		/// 获取编辑 ArticleCategory
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetArticleCategoryForEditOutput> GetForEdit(NullableIdDto<int> input)
		{
			var output = new GetArticleCategoryForEditOutput();
ArticleCategoryEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<ArticleCategoryEditDto>();

				//articleCategoryEditDto = ObjectMapper.Map<List<articleCategoryEditDto>>(entity);
			}
			else
			{
				editDto = new ArticleCategoryEditDto();
			}

			output.ArticleCategory = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改ArticleCategory的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateArticleCategoryInput input)
		{

			if (input.ArticleCategory.Id.HasValue)
			{
				await Update(input.ArticleCategory);
			}
			else
			{
				await Create(input.ArticleCategory);
			}
		}


		/// <summary>
		/// 新增ArticleCategory
		/// </summary>
		
		protected virtual async Task<ArticleCategoryEditDto> Create(ArticleCategoryEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <ArticleCategory>(input);
            var entity=input.MapTo<ArticleCategory>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<ArticleCategoryEditDto>();
		}

		/// <summary>
		/// 编辑ArticleCategory
		/// </summary>
		
		protected virtual async Task Update(ArticleCategoryEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除ArticleCategory信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除ArticleCategory的方法
		/// </summary>
		
		public async Task BatchDelete(List<int> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        public async Task<List<ArticleCategoryListDto>> GetAllTags()
        {
            var entities = await _entityRepository.GetAllListAsync();
            return ObjectMapper.Map<List<ArticleCategoryListDto>>(entities);
        }

        /// <summary>
        /// 导出ArticleCategory为excel表,等待开发。
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


