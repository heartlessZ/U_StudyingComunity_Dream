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
        待审核 = 0,
        草稿=1,
        审核通过=2,
        已删除=3
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderType
    {
        女 = 0,
        男 = 1
    }

    public enum BookResourceStatus
    {
        拒绝=-1,
        待审核=0,
        审核通过=1
    }
}
