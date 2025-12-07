using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer
{
    public class WarningPageRequestDto
    {
        public int Page { get; set; }
        public Guid UserGuid { get; set; }
    }
}
