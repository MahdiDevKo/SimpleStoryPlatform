using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer
{
    public class StoryPlayListManageDto
    {
        public Guid storyGuid { get; set; }
        public Guid? playListGuid { get; set; }
        public bool IsAdd { get; set; }
    }
}
