/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/6 16:42:45
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace U_StudyingCommunity_Dream.Authorization.UserDetails
{
    /// <summary>
    /// 用户详情
    /// </summary>
    [Table("userDetails")]
    public class UserDetail:FullAuditedEntity<Guid>
    {
        [Required]
        [StringLength(20)]
        public string Surname { get; set; }

        [StringLength(100)]
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
        [StringLength(200)]
        public string headPortraitUrl { get; set; }
    }
}
