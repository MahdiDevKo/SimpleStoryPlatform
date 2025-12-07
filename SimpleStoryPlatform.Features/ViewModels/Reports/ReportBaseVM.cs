using SimpleStoryPlatform.Application.DTOs;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.ViewModels.Reports
{
    public class BaseReportVM : BaseDtoInfo
    {
        public string? ReportReason { get; set; }
        public UserWithWarningsDto TargetUser { get; set; }
    }
}
