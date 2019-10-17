
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


using U_StudyingCommunity_Dream.Books;
using U_StudyingCommunity_Dream.Books.Dtos;




namespace U_StudyingCommunity_Dream.Books
{
    /// <summary>
    /// BookCategory应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class BookCategoryAppService : U_StudyingCommunity_DreamAppServiceBase, IBookCategoryAppService
    {
        private readonly IRepository<BookCategory, int> _entityRepository;

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public BookCategoryAppService(
        IRepository<BookCategory, int> entityRepository
        
        )
        {
            _entityRepository = entityRepository; 
            
        }


        /// <summary>
        /// 获取BookCategory的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<BookCategoryListDto>> GetPaged(GetBookCategorysInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<BookCategoryListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<BookCategoryListDto>>();

			return new PagedResultDto<BookCategoryListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取BookCategoryListDto信息
		/// </summary>
		 
		public async Task<BookCategoryListDto> GetById(EntityDto<int> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<BookCategoryListDto>();
		}

		/// <summary>
		/// 获取编辑 BookCategory
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetBookCategoryForEditOutput> GetForEdit(NullableIdDto<int> input)
		{
			var output = new GetBookCategoryForEditOutput();
BookCategoryEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<BookCategoryEditDto>();

				//bookCategoryEditDto = ObjectMapper.Map<List<bookCategoryEditDto>>(entity);
			}
			else
			{
				editDto = new BookCategoryEditDto();
			}

			output.BookCategory = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改BookCategory的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateBookCategoryInput input)
		{

			if (input.BookCategory.Id.HasValue)
			{
				await Update(input.BookCategory);
			}
			else
			{
				await Create(input.BookCategory);
			}
		}


		/// <summary>
		/// 新增BookCategory
		/// </summary>
		
		protected virtual async Task<BookCategoryEditDto> Create(BookCategoryEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <BookCategory>(input);
            var entity=input.MapTo<BookCategory>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<BookCategoryEditDto>();
		}

		/// <summary>
		/// 编辑BookCategory
		/// </summary>
		
		protected virtual async Task Update(BookCategoryEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除BookCategory信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除BookCategory的方法
		/// </summary>
		
		public async Task BatchDelete(List<int> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出BookCategory为excel表,等待开发。
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


