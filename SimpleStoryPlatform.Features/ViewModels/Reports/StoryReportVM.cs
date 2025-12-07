using SimpleStoryPlatform.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.ViewModels.Reports
{
    public class StoryReportVM : BaseReportVM
    {
        public Guid StoryGuid { get; set; }
    }
}
