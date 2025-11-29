using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs
{
    public class StorySearchOptionsDto : BaseSearchOptionsDto
    {
        public string? StoryName { get; set; }
        public string? WriterUsername { get; set; }
    }
}
