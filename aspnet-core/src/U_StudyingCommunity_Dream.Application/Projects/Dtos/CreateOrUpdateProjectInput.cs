

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Projects;

namespace U_StudyingCommunity_Dream.Projects.Dtos
{
    public class CreateOrUpdateProjectInput
    {
        [Required]
        public ProjectEditDto Project { get; set; }

    }
}