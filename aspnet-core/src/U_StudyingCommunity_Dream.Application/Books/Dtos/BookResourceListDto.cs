

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Books;
using U_StudyingCommunity_Dream.Enums;
using Abp.AutoMapper;

namespace U_StudyingCommunity_Dream.Books.Dtos
{

    [AutoMapFrom(typeof(BookResource))]
    public class BookResourceListDto : FullAuditedEntityDto<long> 
    {

        
		/// <summary>
		/// BookId
		/// </summary>
		public long BookId { get; set; }



		/// <summary>
		/// Url
		/// </summary>
		[Required(ErrorMessage="Url不能为空")]
		public string Url { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// Uploader
		/// </summary>
		public Guid? Uploader { get; set; }



		/// <summary>
		/// Auditor
		/// </summary>
		public Guid? Auditor { get; set; }



		/// <summary>
		/// Status
		/// </summary>
		public BookResourceStatus Status { get; set; }


        public string BookName { get; set; }

        public string UploaderName { get; set; }

        public string AuditorName { get; set; }

    }
}