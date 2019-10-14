
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
    /// UserDetail应用层服务的接口方法
    ///</summary>
    public interface IUserDetailAppService : IApplicationService
    {
        /// <summary>
		/// 获取UserDetail的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<UserDetailListDto>> GetPaged(GetUserDetailsInput input);


		/// <summary>
		/// 通过指定id获取UserDetailListDto信息
		/// </summary>
		Task<UserDetailListDto> GetById(EntityDto<Guid> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetUserDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input);


        /// <summary>
        /// 添加或者修改UserDetail的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateUserDetailInput input);


        /// <summary>
        /// 删除UserDetail信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<Guid> input);


        /// <summary>
        /// 批量删除UserDetail
        /// </summary>
        Task BatchDelete(List<Guid> input);


		/// <summary>
        /// 导出UserDetail为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
