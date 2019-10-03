using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using U_StudyingCommunity_Dream.Configuration;
using U_StudyingCommunity_Dream.Web;

namespace U_StudyingCommunity_Dream.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class U_StudyingCommunity_DreamDbContextFactory : IDesignTimeDbContextFactory<U_StudyingCommunity_DreamDbContext>
    {
        public U_StudyingCommunity_DreamDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<U_StudyingCommunity_DreamDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            U_StudyingCommunity_DreamDbContextConfigurer.Configure(builder, configuration.GetConnectionString(U_StudyingCommunity_DreamConsts.ConnectionStringName));

            return new U_StudyingCommunity_DreamDbContext(builder.Options);
        }
    }
}
