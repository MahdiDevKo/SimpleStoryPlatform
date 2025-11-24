using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser
{
    public class UserDetailsDto : BaseDtoInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<NotificationDto>? Inbox { get; set; }
        public List<WarningDto>? Warnings { get; set; }
    }
}
