
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
using Abp.ObjectMapping;

namespace U_StudyingCommunity_Dream.Books
{
    /// <summary>
    /// Book应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class BookAppService : U_StudyingCommunity_DreamAppServiceBase, IBookAppService
    {
        private readonly IRepository<Book, long> _entityRepository;
        private readonly IRepository<BookCategory, int> _categoryRepository;



        /// <summary>
        /// 构造函数 
        ///</summary>
        public BookAppService(
        IRepository<Book, long> entityRepository
        , IRepository<BookCategory, int> categoryRepository
        , IObjectMapper objectMapper
        )
        {
            _entityRepository = entityRepository;
            _categoryRepository = categoryRepository;
            ObjectMapper = objectMapper;
        }


        /// <summary>
        /// 获取Book的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<PagedResultDto<BookListDto>> GetPaged(GetBooksInput input)
        {
            var categoryIds = new List<int>();
            if (input.CategoryId.HasValue)
            {
                GetChildrenCategoryIds(input.CategoryId.Value, ref categoryIds);
                categoryIds.Add(input.CategoryId.Value);
            }

            var query = _entityRepository.GetAll()
                .Where(b => b.Status == input.Status)
                .WhereIf(!string.IsNullOrEmpty(input.Keyword)
                , b => b.Name.Contains(input.Keyword) || b.Author.Contains(input.Keyword))
                .WhereIf(input.CategoryId.HasValue, i => categoryIds.Contains(i.CategoryId));
            // TODO:根据传入的参数添加过滤条件
            var categories = _categoryRepository.GetAll();

            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            var entityListDtos = ObjectMapper.Map<List<BookListDto>>(entityList);
            //var entityListDtos =entityList.MapTo<List<BookListDto>>();

            foreach (var item in entityListDtos)
            {
                var category = categories.First(c => c.Id == item.CategoryId);
                if (category != null)
                {
                    item.CategoryName = category.Name;
                }
            }
            return new PagedResultDto<BookListDto>(count, entityListDtos);
        }

        private void GetChildrenCategoryIds(int id, ref List<int> ids)
        {
            var categorieIds = _categoryRepository.GetAll().Where(c => c.Parent == id).Select(i => i.Id);
            foreach (var item in categorieIds)
            {
                ids.Add(item);
                GetChildrenCategoryIds(item, ref ids);
            }
        }

        /// <summary>
        /// 通过指定id获取BookListDto信息
        /// </summary>

        [AbpAllowAnonymous]
        public async Task<BookListDto> GetById(EntityDto<long> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            var result = ObjectMapper.Map<BookListDto>(entity);

            if (result != null)
            {
                var category = await _categoryRepository.GetAsync(entity.CategoryId);
                if (category != null)
                {
                    result.CategoryName = category.Name;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取编辑 Book
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetBookForEditOutput> GetForEdit(NullableIdDto<long> input)
        {
            var output = new GetBookForEditOutput();
            BookEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<BookEditDto>();

                //bookEditDto = ObjectMapper.Map<List<bookEditDto>>(entity);
            }
            else
            {
                editDto = new BookEditDto();
            }

            output.Book = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Book的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateBookInput input)
        {
            var book = await _entityRepository.GetAll()
                .WhereIf(input.Book.Id.HasValue,i=>i.Id!=input.Book.Id)
                .FirstOrDefaultAsync(i => i.Name == input.Book.Name);
            if (book != null)
            {
                throw new UserFriendlyException("已存在该书籍。");
            }
            if (input.Book.Id.HasValue)
            {
                await Update(input.Book);
            }
            else
            {
                await Create(input.Book);
            }
        }


        /// <summary>
        /// 新增Book
        /// </summary>

        protected virtual async Task<BookEditDto> Create(BookEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map<Book>(input);
            //var entity=input.MapTo<Book>();

            entity = await _entityRepository.InsertAsync(entity);
            //return entity.MapTo<BookEditDto>();
            //return ObjectMapper.Map<BookEditDto>(entity);
            return input;
        }

        /// <summary>
        /// 编辑Book
        /// </summary>

        protected virtual async Task Update(BookEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            //input.MapTo(entity);

            ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Book信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Book的方法
        /// </summary>

        public async Task BatchDelete(List<long> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        /// <summary>
        /// 获取书籍基本信息top10
        /// </summary>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<List<BookSimpleInfoDto>> GetBookSimpleInfos(string Keyword)
        {
            var result = new List<BookSimpleInfoDto>();
            var query = _entityRepository.GetAll()
                .Where(i => i.Status == Enums.BookResourceStatus.审核通过)
                .WhereIf(!string.IsNullOrEmpty(Keyword), i => i.Name.Contains(Keyword)||i.Author.Contains(Keyword));
            var entityies = await query.OrderByDescending(i => i.Praise)
                .Take(5)
                .ToListAsync();
            foreach (var book in entityies)
            {
                result.Add(new BookSimpleInfoDto
                {
                    Id = book.Id,
                    Author = book.Author,
                    Name = book.Name,
                    CoverUrl = book.CoverUrl,
                    Praise = book.Praise
                });
            }
            return result;
        }

        public async Task<bool> CreatePraise(EntityDto<long> input)
        {
            var book = await _entityRepository.GetAsync(input.Id);
            book.Praise++;
            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 导出Book为excel表,等待开发。
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


