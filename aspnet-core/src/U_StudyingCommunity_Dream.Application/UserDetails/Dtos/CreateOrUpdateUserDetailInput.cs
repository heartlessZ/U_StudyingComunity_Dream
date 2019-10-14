

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.UserDetails;

namespace U_StudyingCommunity_Dream.UserDetails.Dtos
{
    public class CreateOrUpdateUserDetailInput
    {
        [Required]
        public UserDetailEditDto UserDetail { get; set; }

    }
}