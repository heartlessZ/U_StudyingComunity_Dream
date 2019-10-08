/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 13:29:38
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace U_StudyingCommunity_Dream.Articles
{
    /// <summary>
    /// 评论表
    /// </summary>
    [Table("comments")]
    public class Comment: FullAuditedEntity<long>
    {
        [Required]
        [StringLength(200)]
        public virtual string Content { get; set; }

        public virtual Guid UserDetailId { get; set; }

        public virtual long Parent { get; set; }

        public virtual long ArticleId { get; set; }
    }
}
