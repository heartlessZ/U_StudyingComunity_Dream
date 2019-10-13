

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Articles;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{
    public class CreateOrUpdateArticleInput
    {
        [Required]
        public ArticleEditDto Article { get; set; }

    }
}