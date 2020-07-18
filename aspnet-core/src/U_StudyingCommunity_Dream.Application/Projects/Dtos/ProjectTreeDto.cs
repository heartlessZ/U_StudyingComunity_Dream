/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/29 14:07:06
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace U_StudyingCommunity_Dream.Projects.Dtos
{
    /// <summary>
    /// Summary description for ProjectTreeDto
    /// </summary>
    public class ProjectTreeDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
        
        /// <summary>
        /// Progress
        /// </summary>
        public decimal Progress { get; set; }
        
        /// <summary>
        /// Parent
        /// </summary>
        public long Parent { get; set; }
        
        /// <summary>
        /// ExpirationTime
        /// </summary>
        public DateTime ExpirationTime { get; set; }
        
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

        public ProjectTreeDto ChildProject { get; set; }
    }
}
