using SimpleStoryPlatform.Application.DTOs;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.ViewModels.Reports
{
    public class ReleaseRequestVM : BaseDtoInfo
    {
        public string RequestMessage { get; set; }
        public bool IsComplete { get; set; }
        public Guid StoryGuid { get; set; }

    }
}
