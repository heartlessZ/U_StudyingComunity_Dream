using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using U_StudyingCommunity_Dream.Authorization.Roles;
using U_StudyingCommunity_Dream.Sessions.Dto;

namespace U_StudyingCommunity_Dream.Sessions
{
    public class SessionAppService : U_StudyingCommunity_DreamAppServiceBase, ISessionAppService
    {
        private readonly IRepository<UserRole,long> _userRolesRepository;

        public SessionAppService(IRepository<UserRole, long> userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;
        }

        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                var user = await GetCurrentUserAsync();
                output.User = ObjectMapper.Map<UserLoginInfoDto>(user);
                var test = _userRolesRepository.GetAll();
                //角色名
                var userRoles = await _userRolesRepository.GetAll().Where(r => r.UserId == user.Id).ToListAsync();
                if (userRoles.Count > 0)
                {
                    output.User.RoleIds = userRoles.Select(r => r.RoleId).ToArray();
                }
            }

            return output;
        }
    }
}
