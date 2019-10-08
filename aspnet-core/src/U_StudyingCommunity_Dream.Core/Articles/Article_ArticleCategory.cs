/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 13:23:22
* DESC: <DESCRIPTION>
* **************************************************************/
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace U_StudyingCommunity_Dream.Articles
{
    /// <summary>
    /// 文章标签中间表
    /// </summary>
    [Table("article_ArticleCategories")]
    public class Article_ArticleCategory:Entity<long>
    {
        public virtual long ArticleId { get; set; }

        public virtual int ArticleCategoryId { get; set; }
    }
}
