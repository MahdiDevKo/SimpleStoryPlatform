using SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser
{
    public class AllReportsDto
    {
        public List<ReviewReportDto>? ReviewReports { get; set; }
        public List<StoryReportDto>? StoryReports { get; set; }
        public List<StoryReleaseRequestDetailsDto>? StoryReleaseRequests { get; set; }
    }
}
