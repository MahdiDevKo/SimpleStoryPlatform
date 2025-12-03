using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators.RuleCore
{
    public interface IStoryNameAndPreivewRule
    {
        public string Name { get; set; }
        public string Preview { get; set; }
    }
}
