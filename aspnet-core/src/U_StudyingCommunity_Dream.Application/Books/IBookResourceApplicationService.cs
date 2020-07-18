
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
using U_StudyingCommunity_Dream.Enums;

namespace U_StudyingCommunity_Dream.Books
{
    /// <summary>
    /// BookResource应用层服务的接口方法
    ///</summary>
    public interface IBookResourceAppService : IApplicationService
    {
        /// <summary>
		/// 获取BookResource的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BookResourceListDto>> GetPaged(GetBookResourcesInput input);


		/// <summary>
		/// 通过指定id获取BookResourceListDto信息
		/// </summary>
		Task<BookResourceListDto> GetById(EntityDto<long> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBookResourceForEditOutput> GetForEdit(NullableIdDto<long> input);


        /// <summary>
        /// 添加或者修改BookResource的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateBookResourceInput input);


        /// <summary>
        /// 删除BookResource信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<long> input);


        /// <summary>
        /// 批量删除BookResource
        /// </summary>
        Task BatchDelete(List<long> input);

        /// <summary>
        /// 通过书籍Id查询书籍资源集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<BookResourceListDto>> GetResourceListByBookId(EntityDto<long> input, BookResourceStatus? status);

		/// <summary>
        /// 导出BookResource为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
