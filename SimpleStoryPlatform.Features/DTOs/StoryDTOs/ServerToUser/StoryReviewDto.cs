using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser
{
    public class StoryReviewDto : BaseDtoInfo
    {
        public UserPreviewDto User { get; set; }
        public Guid TargetStoryGuid { get; set; }
        public float Score { get; set; }
        public string Data { get; set; }
    }
}
