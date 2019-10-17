/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 14:03:34
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace U_StudyingCommunity_Dream.Books
{
    /// <summary>
    /// 书籍分类
    /// </summary>
    [Table("bookCategories")]
    public class BookCategory:Entity<int>,IHasCreationTime
    {
        [Required]
        [StringLength(20)]
        public virtual string Name { get; set; }

        public virtual int Parent { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
