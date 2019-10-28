/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/28 9:37:14
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.Books.Dtos
{
    /// <summary>
    /// Summary description for BookSimpleInfoDto
    /// </summary>
    public class BookSimpleInfoDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
        
        /// <summary>
        /// Author
        /// </summary>
        public string Author { get; set; }
        
        /// <summary>
        /// CoverUrl
        /// </summary>
        public string CoverUrl { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public virtual long Praise { get; set; }
    }
}
