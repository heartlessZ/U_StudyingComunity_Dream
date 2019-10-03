using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using U_StudyingCommunity_Dream.MultiTenancy;

namespace U_StudyingCommunity_Dream.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
