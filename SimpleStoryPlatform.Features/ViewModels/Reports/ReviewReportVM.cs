using SimpleStoryPlatform.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.ViewModels.Reports
{
    public class ReviewReportVM : BaseReportVM
    {
        public string ReviewData { get; set; }
        public Guid ReviewGuid { get; set; }
    }
}
