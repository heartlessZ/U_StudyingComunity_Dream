using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using U_StudyingCommunity_Dream.Authorization.Users;
using U_StudyingCommunity_Dream.MultiTenancy;

namespace U_StudyingCommunity_Dream
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class U_StudyingCommunity_DreamAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected U_StudyingCommunity_DreamAppServiceBase()
        {
            LocalizationSourceName = U_StudyingCommunity_DreamConsts.LocalizationSourceName;
        }

        
        public virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("请先登录!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
