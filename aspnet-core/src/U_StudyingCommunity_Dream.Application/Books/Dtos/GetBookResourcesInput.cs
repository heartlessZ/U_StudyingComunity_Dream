
using Abp.Runtime.Validation;
using U_StudyingCommunity_Dream.Dtos;
using U_StudyingCommunity_Dream.Books;
using U_StudyingCommunity_Dream.Enums;

namespace U_StudyingCommunity_Dream.Books.Dtos
{
    public class GetBookResourcesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public string Name { get; set; }

        public BookResourceStatus Status { get; set; }

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime";
            }
        }

    }
}
