using System.Threading.Tasks;
using Abp.Application.Services;
using U_StudyingCommunity_Dream.Authorization.Accounts.Dto;

namespace U_StudyingCommunity_Dream.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
