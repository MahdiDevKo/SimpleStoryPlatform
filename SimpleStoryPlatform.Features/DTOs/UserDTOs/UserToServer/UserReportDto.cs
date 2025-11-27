using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer
{
    public class UserReportDto      //deosn,t need any validator
    {
        public Guid UserGuid { get; set; }
        public Guid ObjectGuid { get; set; }
        public string? Text { get; set; }
    }
}
