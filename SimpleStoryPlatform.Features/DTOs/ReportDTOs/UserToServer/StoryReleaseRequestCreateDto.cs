using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer
{
    public class StoryReleaseRequestCreateDto
    {
        public Guid StoryGuid { get; set; }
        public string Text { get; set; }
    }
}
