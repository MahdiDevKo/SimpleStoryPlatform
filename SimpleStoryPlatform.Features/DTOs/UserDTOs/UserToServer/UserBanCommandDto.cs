using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer
{
    public class UserBanCommandDto
    {
        public Guid userGuid { get; set; }
        public string? BanReason { get; set; }
        public DateTime UnbanDate { get; set; }
    }
}
