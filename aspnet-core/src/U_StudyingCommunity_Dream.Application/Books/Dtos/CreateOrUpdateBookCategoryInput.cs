

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using U_StudyingCommunity_Dream.Books;

namespace U_StudyingCommunity_Dream.Books.Dtos
{
    public class CreateOrUpdateBookCategoryInput
    {
        [Required]
        public BookCategoryEditDto BookCategory { get; set; }

    }
}