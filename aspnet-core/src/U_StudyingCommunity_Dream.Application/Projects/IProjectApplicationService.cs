
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


using U_StudyingCommunity_Dream.Projects.Dtos;
using U_StudyingCommunity_Dream.Projects;
using U_StudyingCommunity_Dream.Dtos;

namespace U_StudyingCommunity_Dream.Projects
{
    /// <summary>
    /// Project应用层服务的接口方法
    ///</summary>
    public interface IProjectAppService : IApplicationService
    {
        /// <summary>
		/// 获取Project的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ProjectListDto>> GetPaged(GetProjectsInput input);


		/// <summary>
		/// 通过指定id获取ProjectListDto信息
		/// </summary>
		Task<ProjectListDto> GetById(EntityDto<long> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetProjectForEditOutput> GetForEdit(NullableIdDto<long> input);


        /// <summary>
        /// 添加或者修改Project的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<long> CreateOrUpdate(CreateOrUpdateProjectInput input);


        /// <summary>
        /// 删除Project信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<long> input);


        /// <summary>
        /// 批量删除Project
        /// </summary>
        Task BatchDelete(List<long> input);

        /// <summary>
        /// 获取当前用户计划列表
        /// </summary>
        Task<List<ProjectListDto>> GetProjectListDtos();

        Task<ProjectTreeDto> GetProjectTreeById(EntityDto<long> input);
        
		/// <summary>
        /// 导出Project为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
