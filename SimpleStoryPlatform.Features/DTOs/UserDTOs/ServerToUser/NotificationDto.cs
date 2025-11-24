using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser
{
    public class NotificationDto : BaseDtoInfo
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public bool IsReaded { get; set; }
    }
}
