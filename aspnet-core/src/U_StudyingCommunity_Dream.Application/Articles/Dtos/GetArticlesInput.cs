
using Abp.Runtime.Validation;
using U_StudyingCommunity_Dream.Dtos;
using U_StudyingCommunity_Dream.Articles;
using System;
using U_StudyingCommunity_Dream.Enums;

namespace U_StudyingCommunity_Dream.Articles.Dtos
{
    public class GetArticlesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        public Guid? UserDetailId { get; set; }

        public ReleaseStatus? ReleaseStatus { get; set; }

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
