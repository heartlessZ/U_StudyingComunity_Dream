using Microsoft.AspNetCore.Identity;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using U_StudyingCommunity_Dream.Authorization.Roles;
using U_StudyingCommunity_Dream.Authorization.Users;
using U_StudyingCommunity_Dream.MultiTenancy;
using System.Threading.Tasks;
using System;
using U_StudyingCommunity_Dream.UserDetails;
using Abp.UI;

namespace U_StudyingCommunity_Dream.Authorization
{
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        private readonly IRepository<UserDetail, Guid> _userDetailRepository;

        public LogInManager(
            UserManager userManager, 
            IMultiTenancyConfig multiTenancyConfig,
            IRepository<Tenant> tenantRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager, 
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository, 
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            IPasswordHasher<User> passwordHasher, 
            RoleManager roleManager,
            UserClaimsPrincipalFactory claimsPrincipalFactory,
            IRepository<UserDetail, Guid> userDetailRepository
            ) 
            : base(
                  userManager, 
                  multiTenancyConfig,
                  tenantRepository, 
                  unitOfWorkManager, 
                  settingManager, 
                  userLoginAttemptRepository, 
                  userManagementConfig, 
                  iocResolver, 
                  passwordHasher, 
                  roleManager, 
                  claimsPrincipalFactory)
        {
            _userDetailRepository = userDetailRepository;
        }

        public override async Task<AbpLoginResult<Tenant, User>> LoginAsync(string userNameOrEmailAddress, string plainPassword, string tenancyName = null, bool shouldLockout = true)
        {
            var result = await base.LoginAsync(userNameOrEmailAddress, plainPassword, tenancyName, shouldLockout);
            if (result.User != null)
            {
                var userDetail = await _userDetailRepository.GetAsync(result.User.UserDetailId);
                if (userDetail != null)
                {
                    if (!userDetail.Enable)
                    {
                        throw new UserFriendlyException("该账号已被封禁，请联系管理员解决：QQ:1150481513");
                    }
                }
            }

            return result;
        }
    }
}
