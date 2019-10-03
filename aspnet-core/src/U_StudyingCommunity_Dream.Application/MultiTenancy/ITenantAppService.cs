using Abp.Application.Services;
using Abp.Application.Services.Dto;
using U_StudyingCommunity_Dream.MultiTenancy.Dto;

namespace U_StudyingCommunity_Dream.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

