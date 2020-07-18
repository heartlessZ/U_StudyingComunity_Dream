/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 13:27:26
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace U_StudyingCommunity_Dream.Articles
{
    /// <summary>
    /// 文章标签
    /// </summary>
    [Table("articleCategory")]
    public class ArticleCategory:Entity<int>
    {
        [Required]
        [StringLength(20)]
        public virtual string Name { get; set; }
    }
}
