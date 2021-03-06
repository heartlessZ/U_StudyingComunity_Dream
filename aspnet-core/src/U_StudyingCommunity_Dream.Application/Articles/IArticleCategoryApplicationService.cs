
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


using U_StudyingCommunity_Dream.Articles.Dtos;
using U_StudyingCommunity_Dream.Articles;

namespace U_StudyingCommunity_Dream.Articles
{
    /// <summary>
    /// ArticleCategory应用层服务的接口方法
    ///</summary>
    public interface IArticleCategoryAppService : IApplicationService
    {
        /// <summary>
		/// 获取ArticleCategory的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleCategoryListDto>> GetPaged(GetArticleCategorysInput input);


		/// <summary>
		/// 通过指定id获取ArticleCategoryListDto信息
		/// </summary>
		Task<ArticleCategoryListDto> GetById(EntityDto<int> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetArticleCategoryForEditOutput> GetForEdit(NullableIdDto<int> input);


        /// <summary>
        /// 添加或者修改ArticleCategory的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateArticleCategoryInput input);


        /// <summary>
        /// 删除ArticleCategory信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<int> input);


        /// <summary>
        /// 批量删除ArticleCategory
        /// </summary>
        Task BatchDelete(List<int> input);

        Task<List<ArticleCategoryListDto>> GetAllTags();


		/// <summary>
        /// 导出ArticleCategory为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
