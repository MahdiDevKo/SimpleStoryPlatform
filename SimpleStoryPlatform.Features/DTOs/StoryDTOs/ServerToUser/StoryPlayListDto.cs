using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser
{
    public class StoryPlayListDto : BaseDtoInfo
    {
        public string Name { get; set; }
        public PageResponse<StoryPreviewDto> StoriesInPage { get; set; }
    }
}
