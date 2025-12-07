using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites.Report
{
    public class StoryReport : BaseReportEntity
    {
        //releation
        public Story Story { get; set; }
        public int StoryId { get; set; }
    }
}
