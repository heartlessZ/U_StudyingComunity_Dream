/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/18 16:31:03
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Editions;
using Abp.Application.Features;
using U_StudyingCommunity_Dream.Editions;
using U_StudyingCommunity_Dream.Books;

namespace U_StudyingCommunity_Dream.EntityFrameworkCore.Seed.Host
{
    /// <summary>
    /// Summary description for DefaultBookCategoryCreator
    /// </summary>
    public class DefaultBookCategoryCreator
    {
        public static List<BookCategory> InitialBookCategories => GetInitial();

        private readonly U_StudyingCommunity_DreamDbContext _context;

        private static List<BookCategory> GetInitial()
        {
            return new List<BookCategory>
            {
                //顶级类
                //new BookCategory(1,"自然科学",0),
                //new BookCategory(2,"社会科学",0),
                //new BookCategory(3,"综合",0),
                //new BookCategory(4,"哲学",0),
                //new BookCategory(5,"马列主义，毛泽东思想",0),

                //二级类(自然科学子类)
                //new BookCategory(6,"自然科学总论",1),
                //new BookCategory(7,"数理科学和化学",1),
                //new BookCategory(8,"天文学、地球科学",1),
                //new BookCategory(9,"生物科学",1),
                //new BookCategory(10,"医药、卫生",1),
                //new BookCategory(11,"农业科学",1),
                //new BookCategory(12,"工业技术",1),
                //new BookCategory(13,"交通运输",1),
                //new BookCategory(14,"航空、航天",1),
                //new BookCategory(15,"环境科学、安全科学",1),
            };
        }

        public DefaultBookCategoryCreator(U_StudyingCommunity_DreamDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialBookCategories)
            {
                AddBookCategoryIfNotExists(language);
            }
        }

        private void AddBookCategoryIfNotExists(BookCategory category)
        {
            if (_context.Languages.IgnoreQueryFilters().Any(l => l.Name == category.Name))
            {
                return;
            }

            _context.BookCategories.Add(category);
            _context.SaveChanges();
        }
    }
}
