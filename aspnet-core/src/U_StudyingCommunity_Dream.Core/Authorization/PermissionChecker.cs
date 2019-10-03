using Abp.Authorization;
using U_StudyingCommunity_Dream.Authorization.Roles;
using U_StudyingCommunity_Dream.Authorization.Users;

namespace U_StudyingCommunity_Dream.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
