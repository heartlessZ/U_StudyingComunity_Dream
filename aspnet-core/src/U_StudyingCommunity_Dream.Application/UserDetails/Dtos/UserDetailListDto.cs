

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.UserDetails;
using U_StudyingCommunity_Dream.Enums;
using Abp.AutoMapper;

namespace U_StudyingCommunity_Dream.UserDetails.Dtos
{
    [AutoMapFrom(typeof(UserDetail))]
    public class UserDetailListDto : FullAuditedEntityDto<Guid> 
    {

        
		/// <summary>
		/// UserId
		/// </summary>
		public long UserId { get; set; }

        public string Name { get; set; }

		/// <summary>
		/// Surname
		/// </summary>
		[Required(ErrorMessage="Surname不能为空")]
		public string Surname { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }



		/// <summary>
		/// HeadPortraitUrl
		/// </summary>
		public string HeadPortraitUrl { get; set; }



		/// <summary>
		/// Gender
		/// </summary>
		public GenderType? Gender { get; set; }



		/// <summary>
		/// Birthday
		/// </summary>
		public DateTime? Birthday { get; set; }



		/// <summary>
		/// Site
		/// </summary>
		public string Site { get; set; }



		/// <summary>
		/// Occupation
		/// </summary>
		public string Occupation { get; set; }



		/// <summary>
		/// PhoneNumber
		/// </summary>
		public string PhoneNumber { get; set; }



		/// <summary>
		/// Email
		/// </summary>
		public string Email { get; set; }



		/// <summary>
		/// IsAdmin
		/// </summary>
		public bool IsAdmin { get; set; }



        public virtual bool Enable { get; set; }

    }
}