using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser
{
    public class StoryReportDto : BaseDtoInfo
    {
        public Guid StoryGuid { get; set; }
        public string? ReportText { get; set; }
        public bool IsComplete { get; set; }
        public UserWithWarningsDto TargetUser { get; set; }
    }
}
