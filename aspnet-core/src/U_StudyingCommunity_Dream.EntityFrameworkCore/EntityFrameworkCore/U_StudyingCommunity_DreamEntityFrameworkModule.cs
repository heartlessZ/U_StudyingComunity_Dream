using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using U_StudyingCommunity_Dream.EntityFrameworkCore.Seed;

namespace U_StudyingCommunity_Dream.EntityFrameworkCore
{
    [DependsOn(
        typeof(U_StudyingCommunity_DreamCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class U_StudyingCommunity_DreamEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<U_StudyingCommunity_DreamDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        U_StudyingCommunity_DreamDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        U_StudyingCommunity_DreamDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(U_StudyingCommunity_DreamEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
