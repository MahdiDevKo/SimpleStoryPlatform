using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer
{
    public class ReportCompleteDto<T> 
    {
        //need to be COMPLETELY refactored
        public T report { get; set; }
        public Guid ReporterGuid { get; set; }
        public Guid TargetUserGuid { get; set; }
        public Guid ReportGuid { get; set; }
        public bool IsAccepted { get; set; }
        public string? SpicialMessage { get; set; }
    }
}
