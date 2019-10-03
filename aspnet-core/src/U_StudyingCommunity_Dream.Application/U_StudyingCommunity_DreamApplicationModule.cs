using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using U_StudyingCommunity_Dream.Authorization;

namespace U_StudyingCommunity_Dream
{
    [DependsOn(
        typeof(U_StudyingCommunity_DreamCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class U_StudyingCommunity_DreamApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<U_StudyingCommunity_DreamAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(U_StudyingCommunity_DreamApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
