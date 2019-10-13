

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Articles;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{
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




    }
}