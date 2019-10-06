/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/6 20:19:30
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.Dtos
{
    /// <summary>
    /// 通用返回接口
    /// </summary>
    [Serializable]
    public class APIResult
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public Object Data { get; set; }
    }
}
