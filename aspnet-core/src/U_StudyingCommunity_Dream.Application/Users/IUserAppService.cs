using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using U_StudyingCommunity_Dream.Roles.Dto;
using U_StudyingCommunity_Dream.Users.Dto;

namespace U_StudyingCommunity_Dream.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
