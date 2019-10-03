using System.ComponentModel.DataAnnotations;

namespace U_StudyingCommunity_Dream.Configuration.Dto
{
    public class ChangeUiThemeInput
    {
        [Required]
        [StringLength(32)]
        public string Theme { get; set; }
    }
}
