
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using U_StudyingCommunity_Dream.Books;

namespace  U_StudyingCommunity_Dream.Books.Dtos
{

    [AutoMapTo(typeof(BookCategory))]
    public class BookCategoryEditDto : EntityDto<int>, IHasCreationTime
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }         


        
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