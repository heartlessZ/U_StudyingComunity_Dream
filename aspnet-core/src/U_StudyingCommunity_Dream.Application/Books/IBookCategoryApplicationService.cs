
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
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using U_StudyingCommunity_Dream.Books.Dtos;
using U_StudyingCommunity_Dream.Books;

namespace U_StudyingCommunity_Dream.Books
{
    /// <summary>
    /// BookCategory应用层服务的接口方法
    ///</summary>
    public interface IBookCategoryAppService : IApplicationService
    {
        /// <summary>
		/// 获取BookCategory的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BookCategoryListDto>> GetPaged(GetBookCategorysInput input);


		/// <summary>
		/// 通过指定id获取BookCategoryListDto信息
		/// </summary>
		Task<BookCategoryListDto> GetById(EntityDto<int> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBookCategoryForEditOutput> GetForEdit(NullableIdDto<int> input);


        /// <summary>
        /// 添加或者修改BookCategory的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateBookCategoryInput input);


        /// <summary>
        /// 删除BookCategory信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<int> input);


        /// <summary>
        /// 批量删除BookCategory
        /// </summary>
        Task BatchDelete(List<int> input);


        /// <summary>
        /// 获取书籍类别树
        /// </summary>
        /// <returns></returns>
        Task<List<BookCategoryTreeNodesDto>> GetNodes();


		/// <summary>
        /// 导出BookCategory为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
