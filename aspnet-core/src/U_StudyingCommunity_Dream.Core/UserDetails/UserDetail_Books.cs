/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/8 14:12:43
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
    /// 用户收藏书籍
    /// </summary>
    [Table("userDetail_Books")]
    public class UserDetail_Book:Entity<long>
    {
        public virtual Guid UserId { get; set; }

        public virtual long BookId { get; set; }
    }
}
