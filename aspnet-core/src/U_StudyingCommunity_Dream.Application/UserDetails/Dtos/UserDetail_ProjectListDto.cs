

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.UserDetails;
using Abp.AutoMapper;

namespace U_StudyingCommunity_Dream.UserDetails.Dtos
{
    [AutoMapFrom(typeof(UserDetail_Project))]
    public class UserDetail_ProjectListDto : EntityDto<long>,IHasCreationTime 
    {

        
		/// <summary>
		/// UserId
		/// </summary>
		public Guid UserId { get; set; }



		/// <summary>
		/// ProjectId
		/// </summary>
		public long ProjectId { get; set; }



		/// <summary>
		/// TagName
		/// </summary>
		public string TagName { get; set; }



		/// <summary>
		/// IsPublic
		/// </summary>
		public bool IsPublic { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// Praise
		/// </summary>
		public long Praise { get; set; }


        public decimal Progress { get; set; }

    }
}