using System.ComponentModel.DataAnnotations;

namespace U_StudyingCommunity_Dream.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}