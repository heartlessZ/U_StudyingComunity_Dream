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
using U_StudyingCommunity_Dream.Enums;

namespace U_StudyingCommunity_Dream.UserDetails
{
    /// <summary>
    /// 用户详情
    /// </summary>
    [Table("userDetails")]
    public class UserDetail:FullAuditedEntity<Guid>
    {
        public virtual long UserId { get; set; }

        [Required]
        [StringLength(20)]
        public virtual string Surname { get; set; }

        [StringLength(100)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [StringLength(200)]
        public virtual string HeadPortraitUrl { get; set; }

        [StringLength(2)]
        public virtual GenderType? Gender { get; set; }

        public virtual DateTime? Birthday { get; set; }

        [StringLength(100)]
        public virtual string Site { get; set; }

        [StringLength(50)]
        public virtual string Occupation { get; set; }

        [StringLength(11)]
        public virtual string PhoneNumber { get; set; }

        [StringLength(100)]
        public virtual string Email { get; set; }

        public virtual bool IsAdmin { get; set; }

        public UserDetail()
        {
            IsAdmin = false;
        }
    }
}
