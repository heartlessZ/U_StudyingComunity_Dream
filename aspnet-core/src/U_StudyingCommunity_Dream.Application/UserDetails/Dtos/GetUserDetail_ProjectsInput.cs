
using Abp.Runtime.Validation;
using System;
using U_StudyingCommunity_Dream.Dtos;
using U_StudyingCommunity_Dream.UserDetails;

namespace U_StudyingCommunity_Dream.UserDetails.Dtos
{
    public class GetUserDetail_ProjectsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        public Guid? UserDetailId { get; set; }

        public bool? IsPublic { get; set; }

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
