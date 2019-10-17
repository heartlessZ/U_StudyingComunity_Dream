

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Books;

namespace U_StudyingCommunity_Dream.Books.Dtos
{
    public class CreateOrUpdateBookResourceInput
    {
        [Required]
        public BookResourceEditDto BookResource { get; set; }

    }
}