using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites.Report
{
    public class StoryReviewReport : BaseReportEntity
    {
        public StoryReview Review { get; set; }
        public int ReviewId { get; set; }
    }
}
