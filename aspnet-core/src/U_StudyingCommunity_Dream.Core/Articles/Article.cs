/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 13:09:35
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using U_StudyingCommunity_Dream.Enums;

namespace U_StudyingCommunity_Dream.Articles
{
    /// <summary>
    /// 文章
    /// </summary>
    [Table("articles")]
    public class Article : FullAuditedEntity<long>
    {
        [Required]
        [StringLength(100)]
        public virtual string Headline { get; set; }

        [Required]
        public virtual string Content { get; set; }

        public virtual long Praise { get; set; }

        public virtual long VisitVolume { get; set; }

        public virtual ReleaseStatus ReleaseStatus { get; set; }

        public virtual Guid UserDetailId { get; set; }
    }
}
