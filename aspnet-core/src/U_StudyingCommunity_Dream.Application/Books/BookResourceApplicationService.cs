
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
using U_StudyingCommunity_Dream.Enums;
using U_StudyingCommunity_Dream.UserDetails;

namespace U_StudyingCommunity_Dream.Books
{
    /// <summary>
    /// BookResource应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class BookResourceAppService : U_StudyingCommunity_DreamAppServiceBase, IBookResourceAppService
    {
        private readonly IRepository<BookResource, long> _entityRepository;
        private readonly IRepository<UserDetail, Guid> _userDetailRepository;
        private readonly IRepository<Book, long> _bookRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public BookResourceAppService(
        IRepository<BookResource, long> entityRepository
        , IRepository<UserDetail, Guid> userDetailRepository
        , IRepository<Book, long> bookRepository
        )
        {
            _entityRepository = entityRepository;
            _userDetailRepository = userDetailRepository;
            _bookRepository = bookRepository;
        }


        /// <summary>
        /// 获取BookResource的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<BookResourceListDto>> GetPaged(GetBookResourcesInput input)
		{

		    var query = _entityRepository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.Name),b=>b.Name.Contains(input.Name))
                .Where(b=>b.Status == input.Status);
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			var entityListDtos = ObjectMapper.Map<List<BookResourceListDto>>(entityList);
            foreach (var dto in entityListDtos)
            {
                if (dto.Uploader.HasValue)
                {
                    var userDetail = await _userDetailRepository.GetAsync(dto.Uploader.Value);
                    dto.UploaderName = userDetail.Surname;
                }
                if (dto.Auditor.HasValue)
                {
                    var userDetail = await _userDetailRepository.GetAsync(dto.Auditor.Value);
                    dto.AuditorName = userDetail.Surname;
                }
                var book = await _bookRepository.FirstOrDefaultAsync(i=>i.Id==dto.BookId);
                if (book!=null)
                {
                    dto.BookName = book.Name;
                }
            }
			//var entityListDtos =entityList.MapTo<List<BookResourceListDto>>();

			return new PagedResultDto<BookResourceListDto>(count,entityListDtos);
		}


        /// <summary>
        /// 通过指定id获取BookResourceListDto信息
        /// </summary>

        [AbpAllowAnonymous]
        public async Task<BookResourceListDto> GetById(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    //return entity.MapTo<BookResourceListDto>();
            return ObjectMapper.Map<BookResourceListDto>(entity);
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

            var entity = ObjectMapper.Map<BookResource>(input);
            //var entity=input.MapTo<BookResource>();
			

			entity = await _entityRepository.InsertAsync(entity);
            //return entity.MapTo<BookResourceEditDto>();
            return input;
		}

		/// <summary>
		/// 编辑BookResource
		/// </summary>
		
		protected virtual async Task Update(BookResourceEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			//input.MapTo(entity);

			ObjectMapper.Map(input, entity);
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

        [AbpAllowAnonymous]
        public async Task<List<BookResourceListDto>> GetResourceListByBookId(EntityDto<long> input, BookResourceStatus? status)
        {
            var entities = await _entityRepository.GetAll()
                .Where(i => i.BookId == input.Id)
                .WhereIf(status.HasValue, i => i.Status == status)
                .ToListAsync();
            
            var result = ObjectMapper.Map<List<BookResourceListDto>>(entities);
            foreach (var item in result)
            {
                if (item.Uploader.HasValue)
                {
                    var user = await _userDetailRepository.GetAsync(item.Uploader.Value);
                    item.UploaderName = user.Surname;
                }
                if (item.Auditor.HasValue)
                {
                    var user = await _userDetailRepository.GetAsync(item.Uploader.Value);
                    item.AuditorName = user.Surname;
                }
            }
            return result;
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


