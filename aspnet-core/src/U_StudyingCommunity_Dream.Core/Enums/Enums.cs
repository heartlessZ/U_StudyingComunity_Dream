/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/6 16:26:03
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.Enums
{
    /// <summary>
    /// 文章状态
    /// </summary>
    public enum ReleaseStatus
    {
        待审核 = 1,
        审核通过 = 2,
        拒绝 = 3,
        草稿 = 4
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderType
    {
        女 = 1,
        男 = 2
    }

    public enum BookResourceStatus
    {
        待审核 = 1,
        审核通过 = 2,
        拒绝 = 3,
    }
}
