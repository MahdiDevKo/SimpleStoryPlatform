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
    public class StoryPreviewDto : BaseDtoInfo
    {
        public string Name { get; set; }
        public string Preview { get; set; }
        public bool IsPublished { get; set; }
        public bool IsStriked { get; set; }
        public bool IsVisible => IsPublished && !IsStriked;

        public UserPreviewDto Writer { get; set; }
        public int SectionsCount { get; set; }
        public int ReviewsCount { get; set; }
        public Guid? PlayListGuid { get; set; }
    }
}
