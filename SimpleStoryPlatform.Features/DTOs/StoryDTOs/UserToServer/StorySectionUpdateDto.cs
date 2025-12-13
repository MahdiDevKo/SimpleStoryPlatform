using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer
{
    public class StorySectionUpdateDto : StorySectionDto
    {
        public string? Id { get; set; }
    }
}
