

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Books;

namespace U_StudyingCommunity_Dream.Books.Dtos
{
    public class BookCategoryListDto : EntityDto<int>,IHasCreationTime 
    {

        
		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// Parent
		/// </summary>
		public int Parent { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }




    }
}