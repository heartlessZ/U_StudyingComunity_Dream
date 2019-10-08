/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 14:08:25
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
    /// 书籍资源
    /// </summary>
    [Table("bookResource")]
    public class BookResource: FullAuditedEntity<long>
    {
        [Required]
        [StringLength(200)]
        public virtual string Url { get; set; }

        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }

        public virtual BookResourceStatus Status { get; set; }

        public BookResource()
        {
            Status = BookResourceStatus.待审核;
        }
    }
}
