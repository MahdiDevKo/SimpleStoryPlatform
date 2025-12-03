using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators.RuleCore;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser
{
    public class StoryDetailsDto : BaseDtoInfo
    {
        public string Name { get; set; }
        public string Preview { get; set; }
        public UserPreviewDto Writer { get; set; }
        public List<StorySectionDto> Data { get; set; }
        public List<StoryReviewDto>? Reviews { get; set; }
        public bool IsPublished { get; set; }
        public bool IsVisible { get; set; }
        public Guid? PlayListGuid { get; set; }
    }
}
