/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/19 11:31:42
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.Books.Dtos
{
    /// <summary>
    /// 书籍类别树
    /// </summary>
    public class BookCategoryTreeNodesDto
    {
        public string Title { get; set; }

        public int Key { get; set; }

        public int Parent { get; set; }

        public List<BookCategoryTreeNodesDto> Children { get; set; }
    }
}
