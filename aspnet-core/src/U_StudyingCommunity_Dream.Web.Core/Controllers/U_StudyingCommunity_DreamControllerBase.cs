using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace U_StudyingCommunity_Dream.Controllers
{
    public abstract class U_StudyingCommunity_DreamControllerBase: AbpController
    {
        protected U_StudyingCommunity_DreamControllerBase()
        {
            LocalizationSourceName = U_StudyingCommunity_DreamConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
