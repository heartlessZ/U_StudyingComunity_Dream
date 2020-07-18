

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Projects;
using Abp.AutoMapper;

namespace U_StudyingCommunity_Dream.Projects.Dtos
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectListDto : FullAuditedEntityDto<long> 
    {

        
		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// Progress
		/// </summary>
		public decimal Progress { get; set; }



		/// <summary>
		/// Parent
		/// </summary>
		public long Parent { get; set; }



		/// <summary>
		/// ExpirationTime
		/// </summary>
		public DateTime ExpirationTime { get; set; }



		/// <summary>
		/// Remark
		/// </summary>
		public string Remark { get; set; }



        public string TagName { get; set; }

        public bool isPublic { get; set; }

        public Guid? UserDetailId { get; set; }


    }
}