using System.Threading.Tasks;
using Abp.Application.Services;
using U_StudyingCommunity_Dream.Sessions.Dto;

namespace U_StudyingCommunity_Dream.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
