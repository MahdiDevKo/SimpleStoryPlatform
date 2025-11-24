using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites
{
    public  class StorySection : BaseDomainEntity
    {
        public string? Narration { get; set; }
        public string? BGImageUrl { get; set; }
        public string? BGMusicUrl { get; set; }

        //relations
        public Story BelongTo { get; set; }
        public int StoryId { get; set; }
    }
}
