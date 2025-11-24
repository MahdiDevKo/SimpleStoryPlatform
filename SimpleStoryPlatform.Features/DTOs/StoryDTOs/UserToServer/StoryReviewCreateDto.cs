using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer
{
    public class StoryReviewCreateDto
    {
        public float Score { get; set; }
        public string Data { get; set; }
        public Guid StoryGuid { get; set; }
        public Guid UserGuid { get; set; }
    }
}
