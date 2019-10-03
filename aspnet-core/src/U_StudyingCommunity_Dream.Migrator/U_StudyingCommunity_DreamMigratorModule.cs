using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using U_StudyingCommunity_Dream.Configuration;
using U_StudyingCommunity_Dream.EntityFrameworkCore;
using U_StudyingCommunity_Dream.Migrator.DependencyInjection;

namespace U_StudyingCommunity_Dream.Migrator
{
    [DependsOn(typeof(U_StudyingCommunity_DreamEntityFrameworkModule))]
    public class U_StudyingCommunity_DreamMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public U_StudyingCommunity_DreamMigratorModule(U_StudyingCommunity_DreamEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(U_StudyingCommunity_DreamMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                U_StudyingCommunity_DreamConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(U_StudyingCommunity_DreamMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
