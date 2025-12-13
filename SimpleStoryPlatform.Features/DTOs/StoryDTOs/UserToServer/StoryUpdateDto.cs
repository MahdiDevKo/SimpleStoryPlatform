using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators.RuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer
{
    public class StoryUpdateDto : BaseDtoInfo, IStoryNameAndPreivewRule
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Preview { get; set; }
        public List<StorySectionUpdateDto> Data { get; set; }
        public bool IsPublished { get; set; }
        public Guid? PlayListGuid { get; set; }
    }
}
