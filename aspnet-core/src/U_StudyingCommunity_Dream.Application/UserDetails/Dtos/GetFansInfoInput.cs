/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/30 17:02:36
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using U_StudyingCommunity_Dream.Dtos;

namespace U_StudyingCommunity_Dream.UserDetails.Dtos
{
    /// <summary>
    /// Summary description for GetFansInfoInput
    /// </summary>
    public class GetFansInfoInput: PagedInputDto
    {
        public Guid UserDetailId { get; set; }
    }
}
