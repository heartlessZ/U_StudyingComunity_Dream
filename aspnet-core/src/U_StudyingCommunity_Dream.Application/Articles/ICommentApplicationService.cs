
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
    /// Comment应用层服务的接口方法
    ///</summary>
    public interface ICommentAppService : IApplicationService
    {
        /// <summary>
		/// 获取Comment的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CommentListDto>> GetPaged(GetCommentsInput input);


		/// <summary>
		/// 通过指定id获取CommentListDto信息
		/// </summary>
		Task<CommentListDto> GetById(EntityDto<long> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCommentForEditOutput> GetForEdit(NullableIdDto<long> input);


        /// <summary>
        /// 添加或者修改Comment的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateCommentInput input);


        /// <summary>
        /// 删除Comment信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<long> input);


        /// <summary>
        /// 批量删除Comment
        /// </summary>
        Task BatchDelete(List<long> input);

        /// <summary>
        /// 通过文章Id分页获取评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<MyPageResultDto<CommentsTreeDto>> GetCommentsByArticleId(GetCommentsInput input);


		/// <summary>
        /// 导出Comment为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
