using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser
{
    public class UserProfileDto
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBan { get; set; }
        public DateTime? UnBanDate { get; set; }
        public string? BanReason { get; set; }

        public PageResponse<StoryPreviewDto>? WritedStories { get; set; }
    }
}
