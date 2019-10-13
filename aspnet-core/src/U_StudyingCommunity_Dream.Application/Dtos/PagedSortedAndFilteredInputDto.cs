/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/13 11:21:55
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.Dtos
{
    /// <summary>
    /// 分页实体
    /// </summary>
    public class PagedSortedAndFilteredInputDto: PagedAndSortedInputDto
    {
        public string FilterText { get; set; }
        
    }

}
