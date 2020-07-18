/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 14:34:01
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace U_StudyingCommunity_Dream.UserDetails
{
    /// <summary>
    /// 用户学习计划中间表
    /// </summary>
    [Table("userDetail_Projects")]
    public class UserDetail_Project:Entity<long>,IHasCreationTime
    {
        public virtual Guid UserId { get; set; }

        public virtual long ProjectId { get; set; }

        public virtual string TagName { get; set; }

        public virtual bool IsPublic { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual long Praise { get; set; }
    }
}
