/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 13:34:30
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

namespace U_StudyingCommunity_Dream.Books
{
    /// <summary>
    /// 书籍详情
    /// </summary>
    [Table("books")]
    public class Book: FullAuditedEntity<long>
    {
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(50)]
        public virtual string Author { get; set; }

        [StringLength(500)]
        public virtual string Description { get; set; }

        [StringLength(200)]
        public virtual string CoverUrl { get; set; }
        
        public virtual BookResourceStatus Status { get; set; }

        public Book()
        {
            Status = BookResourceStatus.待审核;
        }
    }
}
