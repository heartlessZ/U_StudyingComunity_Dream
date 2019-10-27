
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using U_StudyingCommunity_Dream.Enums;
using U_StudyingCommunity_Dream.UserDetails;

namespace  U_StudyingCommunity_Dream.UserDetails.Dtos
{
    [AutoMapTo(typeof(UserDetail))]
    public class UserDetailEditDto:FullAuditedEntity<Guid>
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }         


        
		/// <summary>
		/// UserId
		/// </summary>
		public long UserId { get; set; }



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

    public class UpdateStatusInput
    {
        public Guid Id { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? Enable { get; set; }
    }
}