/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/13 11:25:11
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.Dtos
{
    /// <summary>
    /// Summary description for PagedAndSortedInputDto
    /// </summary>
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public string Sorting { get; set; }
        
        public PagedAndSortedInputDto()
        {
            MaxResultCount = AppLtmConsts.DefaultPageSize;
        }
    }

}
