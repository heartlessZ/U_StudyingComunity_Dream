
using Abp.Runtime.Validation;
using U_StudyingCommunity_Dream.Dtos;
using U_StudyingCommunity_Dream.UserDetails;

namespace U_StudyingCommunity_Dream.UserDetails.Dtos
{
    public class GetUserDetailsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        public string Name { get; set; }

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
