

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Articles;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{
    public class CreateOrUpdateCommentInput
    {
        [Required]
        public CommentEditDto Comment { get; set; }

    }
}