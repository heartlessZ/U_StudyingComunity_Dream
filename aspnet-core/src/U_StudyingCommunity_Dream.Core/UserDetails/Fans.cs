/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 14:15:18
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace U_StudyingCommunity_Dream.UserDetails
{
    /// <summary>
    /// 粉丝
    /// </summary>
    [Table("fans")]
    public class Fans:Entity<long>
    {
        public virtual Guid UserId { get; set; }

        public virtual Guid FansId { get; set; }
    }
}
