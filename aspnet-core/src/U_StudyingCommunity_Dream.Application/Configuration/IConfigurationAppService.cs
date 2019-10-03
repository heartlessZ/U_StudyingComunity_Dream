using System.Threading.Tasks;
using U_StudyingCommunity_Dream.Configuration.Dto;

namespace U_StudyingCommunity_Dream.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
