using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators.RuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser
{
    public class StorySectionDto : BaseDtoInfo, IStorySectionRule
    {
        public string? Narration { get; set; }
        public string? BGImageUrl { get; set; }
        public string? BGMusicUrl { get; set; }
    }
}
