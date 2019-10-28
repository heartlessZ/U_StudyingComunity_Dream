/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/28 9:11:53
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using U_StudyingCommunity_Dream.Enums;

namespace U_StudyingCommunity_Dream.UserDetails.Dtos
{
    /// <summary>
    /// Summary description for UserSimpleInfoDto
    /// </summary>
    public class UserSimpleInfoDto
    {
        public Guid Id { get; set; }

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
        /// 粉丝数量
        /// </summary>
        public int FansCount { get; set; }
    }
}
