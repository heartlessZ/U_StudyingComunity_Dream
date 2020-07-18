using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using U_StudyingCommunity_Dream.Authorization.Roles;
using U_StudyingCommunity_Dream.Authorization.Users;
using U_StudyingCommunity_Dream.MultiTenancy;
using U_StudyingCommunity_Dream.UserDetails;
using U_StudyingCommunity_Dream.Articles;
using U_StudyingCommunity_Dream.Books;
using U_StudyingCommunity_Dream.Projects;

namespace U_StudyingCommunity_Dream.EntityFrameworkCore
{
    public class U_StudyingCommunity_DreamDbContext : AbpZeroDbContext<Tenant, Role, User, U_StudyingCommunity_DreamDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public U_StudyingCommunity_DreamDbContext(DbContextOptions<U_StudyingCommunity_DreamDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserDetail_Book> UserDetail_Books { get; set; }
        public virtual DbSet<UserDetail_Project> UserDetail_Projects { get; set; }
        public virtual DbSet<Fans> Fans { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Article_ArticleCategory> Article_ArticleCategories { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<BookResource> BookResources { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
    }
}
