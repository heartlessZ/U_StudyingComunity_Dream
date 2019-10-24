

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Articles;
using Abp.AutoMapper;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{

    [AutoMapFrom(typeof(Comment))]
    public class CommentListDto : FullAuditedEntityDto<long> 
    {

        
		/// <summary>
		/// Content
		/// </summary>
		[Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }



		/// <summary>
		/// UserDetailId
		/// </summary>
		[Required(ErrorMessage="UserDetailId不能为空")]
		public Guid UserDetailId { get; set; }



		/// <summary>
		/// Parent
		/// </summary>
		[Required(ErrorMessage="Parent不能为空")]
		public long Parent { get; set; }



		/// <summary>
		/// ArticleId
		/// </summary>
		public long ArticleId { get; set; }




    }
}