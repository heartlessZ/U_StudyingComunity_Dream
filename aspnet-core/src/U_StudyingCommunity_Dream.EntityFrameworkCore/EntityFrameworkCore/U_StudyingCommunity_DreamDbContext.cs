using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using U_StudyingCommunity_Dream.Authorization.Roles;
using U_StudyingCommunity_Dream.Authorization.Users;
using U_StudyingCommunity_Dream.MultiTenancy;

namespace U_StudyingCommunity_Dream.EntityFrameworkCore
{
    public class U_StudyingCommunity_DreamDbContext : AbpZeroDbContext<Tenant, Role, User, U_StudyingCommunity_DreamDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public U_StudyingCommunity_DreamDbContext(DbContextOptions<U_StudyingCommunity_DreamDbContext> options)
            : base(options)
        {
        }
    }
}
