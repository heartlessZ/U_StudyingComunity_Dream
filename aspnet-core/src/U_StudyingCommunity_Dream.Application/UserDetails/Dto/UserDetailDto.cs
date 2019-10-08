/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/6 19:07:59
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using U_StudyingCommunity_Dream.Enums;

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
        public string HeadPortraitUrl { get; set; }
        
        public virtual GenderType? Gender { get; set; }

        public virtual DateTime? Birthday { get; set; }
        
        public virtual string Site { get; set; }
        
        public virtual string Occupation { get; set; }
        
        public virtual string PhoneNumber { get; set; }
        
        public virtual string Email { get; set; }
    }
}
