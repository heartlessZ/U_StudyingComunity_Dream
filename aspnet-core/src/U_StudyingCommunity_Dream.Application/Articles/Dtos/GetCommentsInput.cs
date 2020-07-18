
using Abp.Runtime.Validation;
using U_StudyingCommunity_Dream.Dtos;
using U_StudyingCommunity_Dream.Articles;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{
    public class GetCommentsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        public long ArticleId { get; set; }

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
