
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


using U_StudyingCommunity_Dream.UserDetails.Dtos;
using U_StudyingCommunity_Dream.UserDetails;

namespace U_StudyingCommunity_Dream.UserDetails
{
    /// <summary>
    /// UserDetail_Project应用层服务的接口方法
    ///</summary>
    public interface IUserDetail_ProjectAppService : IApplicationService
    {
        /// <summary>
		/// 获取UserDetail_Project的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<UserDetail_ProjectListDto>> GetPaged(GetUserDetail_ProjectsInput input);


		/// <summary>
		/// 通过指定id获取UserDetail_ProjectListDto信息
		/// </summary>
		Task<UserDetail_ProjectListDto> GetById(EntityDto<long> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUserDetail_ProjectForEditOutput> GetForEdit(NullableIdDto<long> input);


        /// <summary>
        /// 添加或者修改UserDetail_Project的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateUserDetail_ProjectInput input);


        /// <summary>
        /// 删除UserDetail_Project信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<long> input);


        /// <summary>
        /// 批量删除UserDetail_Project
        /// </summary>
        Task BatchDelete(List<long> input);

        Task<List<UserDetail_ProjectListDto>> GetCurrentUserProjects();

        Task<bool> EditUserProjectProId(EditUserProjectProIdDto input);
        /// <summary>
        /// 导出UserDetail_Project为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
