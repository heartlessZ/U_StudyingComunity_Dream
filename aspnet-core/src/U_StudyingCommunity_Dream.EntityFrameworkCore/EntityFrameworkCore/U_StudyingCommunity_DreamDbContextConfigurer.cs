using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace U_StudyingCommunity_Dream.EntityFrameworkCore
{
    public static class U_StudyingCommunity_DreamDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<U_StudyingCommunity_DreamDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<U_StudyingCommunity_DreamDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
