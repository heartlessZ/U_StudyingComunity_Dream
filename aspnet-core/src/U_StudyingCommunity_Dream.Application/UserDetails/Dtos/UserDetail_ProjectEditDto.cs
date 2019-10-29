
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using U_StudyingCommunity_Dream.UserDetails;

namespace  U_StudyingCommunity_Dream.UserDetails.Dtos
{
    [AutoMapTo(typeof(UserDetail_Project))]
    public class UserDetail_ProjectEditDto : Entity<long>, IHasCreationTime
    {

        /// <summary>
        /// Id
        /// </summary>
        public long? Id { get; set; }         


        
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




    }
}