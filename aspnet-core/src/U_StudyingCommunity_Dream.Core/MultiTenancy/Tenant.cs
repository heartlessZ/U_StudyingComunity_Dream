using Abp.MultiTenancy;
using U_StudyingCommunity_Dream.Authorization.Users;

namespace U_StudyingCommunity_Dream.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
