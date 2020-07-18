
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using U_StudyingCommunity_Dream.Books;
using U_StudyingCommunity_Dream.Enums;

namespace  U_StudyingCommunity_Dream.Books.Dtos
{

    [AutoMapTo(typeof(BookResource))]
    public class BookResourceEditDto : FullAuditedEntity<long>
    {

        /// <summary>
        /// Id
        /// </summary>
        public long? Id { get; set; }         


        
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
        
    }
}