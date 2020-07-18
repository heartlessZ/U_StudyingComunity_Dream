

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Articles;
using U_StudyingCommunity_Dream.Enums;
using Abp.AutoMapper;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{
    [AutoMapFrom(typeof(Article))]
    public class ArticleListDto : FullAuditedEntityDto<long> 
    {

        
		/// <summary>
		/// Headline
		/// </summary>
		[Required(ErrorMessage="Headline不能为空")]
		public string Headline { get; set; }



		/// <summary>
		/// Content
		/// </summary>
		[Required(ErrorMessage="Content不能为空")]
		public string Content { get; set; }



		/// <summary>
		/// Praise
		/// </summary>
		public long Praise { get; set; }



		/// <summary>
		/// VisitVolume
		/// </summary>
		public long VisitVolume { get; set; }



		/// <summary>
		/// ReleaseStatus
		/// </summary>
		public ReleaseStatus ReleaseStatus { get; set; }



		/// <summary>
		/// UserDetailId
		/// </summary>
		public Guid UserDetailId { get; set; }


        public string UserName { get; set; }
        
        /// <summary>
        /// 标签名称集合
        /// </summary>
        public string TagNames { get; set; }

        public string HeadPortraitUrl { get; set; }

        public int[] CategoryIds { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }
        public virtual string Description { get; set; }
    }
}