/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/13 11:25:57
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace U_StudyingCommunity_Dream.Dtos
{
    /// <summary>
    /// Summary description for PagedInputDto
    /// </summary>
    public class PagedInputDto : IPagedResultRequest
    {
        [Range(1, AppLtmConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }
        
        public PagedInputDto()
        {
            MaxResultCount = AppLtmConsts.DefaultPageSize;
        }
    }

}
