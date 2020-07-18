/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/24 9:20:12
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{
    /// <summary>
    /// Summary description for MyPageResultDto
    /// </summary>
    public class MyPageResultDto<T>
    {
        public MyPageResultDto()
        {
        }

        public MyPageResultDto(int totalCount,List<T> items)
        {
            this.TotalCount = totalCount;
            Items = items;
        }

        public int TotalCount { get; set; }

        public List<T> Items { get; set; } 
    }
}
