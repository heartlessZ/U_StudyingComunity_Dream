/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/24 8:44:52
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{
    /// <summary>
    /// 评论的树状结构实体
    /// </summary>
    public class CommentsTreeDto
    {

        public long Id { get; set; }
        /// <summary>
        /// 评论人的名字
        /// </summary>
        public string Author { get; set; }

        public Guid UserDetailId { get; set; }

        public string Avatar { get; set; }

        public string Content { get; set; }

        public DateTime CreationTime { get; set; }

        public List<CommentsTreeDto> Children { get; set; }
    }
}
