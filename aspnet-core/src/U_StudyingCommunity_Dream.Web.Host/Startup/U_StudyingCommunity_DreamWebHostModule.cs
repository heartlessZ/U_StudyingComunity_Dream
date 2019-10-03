using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using U_StudyingCommunity_Dream.Configuration;

namespace U_StudyingCommunity_Dream.Web.Host.Startup
{
    [DependsOn(
       typeof(U_StudyingCommunity_DreamWebCoreModule))]
    public class U_StudyingCommunity_DreamWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public U_StudyingCommunity_DreamWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(U_StudyingCommunity_DreamWebHostModule).GetAssembly());
        }
    }
}
