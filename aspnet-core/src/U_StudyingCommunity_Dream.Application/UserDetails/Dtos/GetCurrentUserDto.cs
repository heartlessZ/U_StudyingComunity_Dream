/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/14 10:49:33
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.UserDetails.Dtos
{
    /// <summary>
    /// 获取当前登录用户基本信息
    /// </summary>
    public class GetCurrentUserDto
    {
        public long UserId { get; set; }

        public string Name { get; set; }

        public Guid UserDetailId { get; set; }

        public string Surname { get; set; }

        public string HeadPortraitUrl { get; set; }

        public bool IsAdmin { get; set; }
    }
}
