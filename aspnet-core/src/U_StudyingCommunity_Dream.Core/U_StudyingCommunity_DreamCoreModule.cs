using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using U_StudyingCommunity_Dream.Authorization.Roles;
using U_StudyingCommunity_Dream.Authorization.Users;
using U_StudyingCommunity_Dream.Configuration;
using U_StudyingCommunity_Dream.Localization;
using U_StudyingCommunity_Dream.MultiTenancy;
using U_StudyingCommunity_Dream.Timing;

namespace U_StudyingCommunity_Dream
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class U_StudyingCommunity_DreamCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            U_StudyingCommunity_DreamLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = U_StudyingCommunity_DreamConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(U_StudyingCommunity_DreamCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
