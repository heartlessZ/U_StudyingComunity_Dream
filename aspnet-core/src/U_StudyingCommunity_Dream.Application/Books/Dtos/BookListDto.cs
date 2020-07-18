

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Books;
using U_StudyingCommunity_Dream.Enums;
using Abp.AutoMapper;

namespace U_StudyingCommunity_Dream.Books.Dtos
{

    [AutoMapFrom(typeof(Book))]
    public class BookListDto : FullAuditedEntityDto<long> 
    {

        
		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// Author
		/// </summary>
		[Required(ErrorMessage="Author不能为空")]
		public string Author { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }



		/// <summary>
		/// CoverUrl
		/// </summary>
		public string CoverUrl { get; set; }



		/// <summary>
		/// OtherUrls
		/// </summary>
		public string OtherUrls { get; set; }



		/// <summary>
		/// CategoryId
		/// </summary>
		public int CategoryId { get; set; }

        public string CategoryName { get; set; }

		/// <summary>
		/// Status
		/// </summary>
		public BookResourceStatus Status { get; set; }




    }
}