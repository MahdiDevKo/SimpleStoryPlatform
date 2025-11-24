using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites
{
    public class StoryReview : BaseDomainEntity
    {
        public float Score { get; set; }
        public string Data { get; set; }

        //relations
        public ICollection<StoryReviewReport> Reports { get; set; } = new List<StoryReviewReport>();
        public Story TargetStory { get; set; }
        public int StoryId { get; set; }
        public User Reviewer { get; set; }
        public int ReviewerId { get; set; }
    }
}
