using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites.Report
{
    public class StoryReleaseRequest : BaseDomainEntity
    {
        public string RequestMessage { get; set; }
        public bool IsComplete { get; set; }
        //relation
        public Story Story { get; set; }
        public int StoryId { get; set; }
    }
}
