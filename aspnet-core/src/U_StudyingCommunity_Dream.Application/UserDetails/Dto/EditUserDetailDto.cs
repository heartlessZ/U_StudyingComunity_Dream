/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/6 19:02:50
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using U_StudyingCommunity_Dream.Authorization.UserDetails;

namespace U_StudyingCommunity_Dream.UserDetails.Dto
{
    /// <summary>
    /// Summary description for EditUserDetailDto
    /// </summary>
    [AutoMapFrom(typeof(UserDetail))]
    public class EditUserDetailDto
    {
        public string Surname { get; set; }
        
        public string Description { get; set; }

        /// <summary>
        /// 粉丝列表
        /// </summary>
        public string Fans { get; set; }

        /// <summary>
        /// 关注列表
        /// </summary>
        public string Idols { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string headPortraitUrl { get; set; }
    }
}
