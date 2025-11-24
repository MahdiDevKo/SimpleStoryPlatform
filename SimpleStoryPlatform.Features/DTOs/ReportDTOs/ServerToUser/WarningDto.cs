using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser
{
    public class WarningDto : BaseDtoInfo
    {
        public string Subject { get; set; }
        public string Reason { get; set; }
        public string? Details { get; set; }
    }
}
