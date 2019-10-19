﻿/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 14:30:06
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace U_StudyingCommunity_Dream.Projects
{
    /// <summary>
    /// 学习计划
    /// </summary>
    [Table("projects")]
    public class Project: FullAuditedEntity<long>
    {
        [Required]
        public virtual Guid Node { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        public virtual decimal Progress { get; set; }
        
        public virtual bool IsPublic { get; set; }

        public virtual Guid? ParentNode { get; set; }
    }
}
