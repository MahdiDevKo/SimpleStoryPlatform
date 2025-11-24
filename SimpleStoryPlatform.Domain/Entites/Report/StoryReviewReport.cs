using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites.Report
{
    public class StoryReviewReport : BaseReportEntity<StoryReview>
    {
        public int ReviewId { get; set; }
    }
}
