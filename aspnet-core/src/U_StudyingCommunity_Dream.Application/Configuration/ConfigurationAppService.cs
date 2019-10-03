using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using U_StudyingCommunity_Dream.Configuration.Dto;

namespace U_StudyingCommunity_Dream.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : U_StudyingCommunity_DreamAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
