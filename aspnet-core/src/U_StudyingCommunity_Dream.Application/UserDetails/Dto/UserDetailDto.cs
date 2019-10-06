/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/6 19:07:59
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.UserDetails.Dto
{
    /// <summary>
    /// 用户详情展示实体
    /// </summary>
    public class UserDetailDto
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
