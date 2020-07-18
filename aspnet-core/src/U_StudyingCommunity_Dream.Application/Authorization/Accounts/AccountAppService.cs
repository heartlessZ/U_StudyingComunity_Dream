using System;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Zero.Configuration;
using U_StudyingCommunity_Dream.Authorization.Accounts.Dto;
using U_StudyingCommunity_Dream.UserDetails;
using U_StudyingCommunity_Dream.Authorization.Users;
using System.Linq;

namespace U_StudyingCommunity_Dream.Authorization.Accounts
{
    public class AccountAppService : U_StudyingCommunity_DreamAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;

        private readonly IRepository<UserDetail, Guid> _userDetailRepository;
        private readonly IRepository<User, long> _userRepository;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager
            , IRepository<UserDetail, Guid> userDetailRepository
            , IRepository<User, long> userRepository)
        {
            _userRegistrationManager = userRegistrationManager;
            _userDetailRepository = userDetailRepository;
            _userRepository = userRepository;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            if (await ExistEmailValid(input.EmailAddress))
                return new RegisterOutput()
                {
                    Code = 999,
                    CanLogin = false,
                    Msg = "该邮箱已经被注册."
                };
            if (await ExistUserNameValid(input.Name))
                return new RegisterOutput()
                {
                    Code =666,
                    CanLogin = false,
                    Msg = "该用户名已经被使用."
                };
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );
            
            var userDetail = new UserDetail()
            {
                Id = user.UserDetailId,
                UserId = user.Id,
                Email = user.EmailAddress,
                Surname = user.Surname,
                Name = user.Name,
                Enable = true
            };

            _userDetailRepository.Insert(userDetail);

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                Code = 200,
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }

        private async Task<bool> ExistEmailValid(string email)
        {
            return await _userRepository.FirstOrDefaultAsync(u => u.EmailAddress != email) == null;
        }

        private async Task<bool> ExistUserNameValid(string name)
        {
            return await _userRepository.FirstOrDefaultAsync(u => u.Name != name) == null;
        }
    }
}
