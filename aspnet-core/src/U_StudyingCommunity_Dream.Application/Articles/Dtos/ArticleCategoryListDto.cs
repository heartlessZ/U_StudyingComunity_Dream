

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Articles;
using Abp.AutoMapper;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{
    [AutoMapFrom(typeof(ArticleCategory))]
    public class ArticleCategoryListDto : EntityDto<int> 
    {

        
		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }




    }
}