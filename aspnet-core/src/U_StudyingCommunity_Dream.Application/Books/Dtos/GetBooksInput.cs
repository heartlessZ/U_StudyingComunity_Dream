
using Abp.Runtime.Validation;
using U_StudyingCommunity_Dream.Dtos;
using U_StudyingCommunity_Dream.Books;

namespace U_StudyingCommunity_Dream.Books.Dtos
{
    public class GetBooksInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string Keyword { get; set; }

        public int? CategoryId { get; set; }
        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

    }
}
