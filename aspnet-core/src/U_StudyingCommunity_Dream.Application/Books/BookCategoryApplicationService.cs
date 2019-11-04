
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
        private readonly IRepository<Book, long> _bookRepository;



        /// <summary>
        /// 构造函数 
        ///</summary>
        public BookCategoryAppService(
        IRepository<BookCategory, int> entityRepository
        , IRepository<Book, long> bookRepository
        )
        {
            _entityRepository = entityRepository;
            _bookRepository = bookRepository;
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

            //var entity = ObjectMapper.Map<BookCategory>(input);
            //var entity=input.MapTo<BookCategory>();
            var entity = new BookCategory()
            {
                Name = input.Name,
                Parent = input.Parent
            };

            entity = await _entityRepository.InsertAsync(entity);

            //ObjectMapper.Map(entity, input);
            return input;
		}

		/// <summary>
		/// 编辑BookCategory
		/// </summary>
		
		protected virtual async Task Update(BookCategoryEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			//input.MapTo(entity);

			 ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除BookCategory信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<bool> Delete(EntityDto<int> input)
		{
            var books = _bookRepository.GetAll().Where(i => i.CategoryId == input.Id);
            if (books.Count() > 0)
            {
                return false;
            }
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
            return true;
		}



		/// <summary>
		/// 批量删除BookCategory的方法
		/// </summary>
		
		public async Task BatchDelete(List<int> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        [AbpAllowAnonymous]
        public async Task<List<BookCategoryTreeNodesDto>> GetNodes()
        {
            var result = new List<BookCategoryTreeNodesDto>();
            var categories = _entityRepository.GetAll().Where(c => c.Parent == 0);
            foreach (var category in categories)
            {
                result.Add(new BookCategoryTreeNodesDto()
                {
                    Key = category.Id,
                    Value = category.Id,
                    Parent = 0,
                    Title = category.Name,
                    Lable = category.Name,
                    Children = GetChildNodes(category.Id)
                });
            }
            return result;
        }

        private List<BookCategoryTreeNodesDto> GetChildNodes(int parent)
        {
            var result = new List<BookCategoryTreeNodesDto>();
            var categories = _entityRepository.GetAll().Where(c => c.Parent == parent);
            foreach (var category in categories)
            {
                result.Add(new BookCategoryTreeNodesDto()
                {
                    Key = category.Id,
                    Value = category.Id,
                    Title = category.Name,
                    Parent = parent,
                    Lable = category.Name,
                    Children = GetChildNodes(category.Id)
                });
            }
            return result;
        }

        public async Task<bool> CheckCanDelete(EntityDto<int> input)
        {
            return await Delete(input);
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


