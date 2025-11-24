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
    public class StoryCreateDto
    {
        [Required(ErrorMessage = "نام داستان نمیتواند خالی باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "خلاصه داستان نمیتواند خالی باشد")]
        public string Preview { get; set; }
        public Guid WriterGuid { get; set; }
    }
}
