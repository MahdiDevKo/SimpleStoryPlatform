using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer
{
    public class ReportCompleteDto
    {
        //deosn't need validator
        public Guid ReportGuid { get; set; }
        public bool IsAccepted { get; set; }
        public string? SpicialMessage { get; set; }
    }
}
