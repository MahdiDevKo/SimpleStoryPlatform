using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators.RuleCore;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer
{
    public class StoryCreateDto : IStoryNameAndPreivewRule
    {
        public string Name { get; set; }
        public string Preview { get; set; }
        public Guid WriterGuid { get; set; }
    }
}
