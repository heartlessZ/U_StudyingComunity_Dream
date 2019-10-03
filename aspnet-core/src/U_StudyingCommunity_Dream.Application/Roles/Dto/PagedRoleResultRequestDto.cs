using Abp.Application.Services.Dto;

namespace U_StudyingCommunity_Dream.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

