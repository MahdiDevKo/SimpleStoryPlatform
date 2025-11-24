using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs
{
    public class BaseDtoInfo
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
