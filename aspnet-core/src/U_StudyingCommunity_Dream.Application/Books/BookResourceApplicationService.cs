
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
    /// BookResource应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class BookResourceAppService : U_StudyingCommunity_DreamAppServiceBase, IBookResourceAppService
    {
        private readonly IRepository<BookResource, long> _entityRepository;

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public BookResourceAppService(
        IRepository<BookResource, long> entityRepository
        
        )
        {
            _entityRepository = entityRepository; 
            
        }


        /// <summary>
        /// 获取BookResource的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<BookResourceListDto>> GetPaged(GetBookResourcesInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<BookResourceListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<BookResourceListDto>>();

			return new PagedResultDto<BookResourceListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取BookResourceListDto信息
		/// </summary>
		 
		public async Task<BookResourceListDto> GetById(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<BookResourceListDto>();
		}

		/// <summary>
		/// 获取编辑 BookResource
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetBookResourceForEditOutput> GetForEdit(NullableIdDto<long> input)
		{
			var output = new GetBookResourceForEditOutput();
BookResourceEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<BookResourceEditDto>();

				//bookResourceEditDto = ObjectMapper.Map<List<bookResourceEditDto>>(entity);
			}
			else
			{
				editDto = new BookResourceEditDto();
			}

			output.BookResource = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改BookResource的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateBookResourceInput input)
		{

			if (input.BookResource.Id.HasValue)
			{
				await Update(input.BookResource);
			}
			else
			{
				await Create(input.BookResource);
			}
		}


		/// <summary>
		/// 新增BookResource
		/// </summary>
		
		protected virtual async Task<BookResourceEditDto> Create(BookResourceEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <BookResource>(input);
            var entity=input.MapTo<BookResource>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<BookResourceEditDto>();
		}

		/// <summary>
		/// 编辑BookResource
		/// </summary>
		
		protected virtual async Task Update(BookResourceEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除BookResource信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除BookResource的方法
		/// </summary>
		
		public async Task BatchDelete(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出BookResource为excel表,等待开发。
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


