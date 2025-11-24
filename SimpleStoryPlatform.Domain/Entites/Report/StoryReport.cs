using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites.Report
{
    public class StoryReport : BaseReportEntity<Story>
    {
        public int StoryId { get; set; }
    }
}
