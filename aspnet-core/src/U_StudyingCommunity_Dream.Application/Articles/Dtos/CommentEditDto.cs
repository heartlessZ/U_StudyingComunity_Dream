
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using U_StudyingCommunity_Dream.Articles;

namespace  U_StudyingCommunity_Dream.Articles.Dtos
{

    [AutoMapTo(typeof(Comment))]
    public class CommentEditDto : FullAuditedEntityDto<long>
    {

        /// <summary>
        /// Id
        /// </summary>
        public long? Id { get; set; }         


        
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