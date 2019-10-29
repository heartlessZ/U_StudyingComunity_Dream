

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.UserDetails;

namespace U_StudyingCommunity_Dream.UserDetails.Dtos
{
    public class CreateOrUpdateUserDetail_ProjectInput
    {
        [Required]
        public UserDetail_ProjectEditDto UserDetail_Project { get; set; }

    }
}