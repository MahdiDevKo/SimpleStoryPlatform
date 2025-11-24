using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Domain.Entites.Report
{
    public class BaseReportEntity<T> : BaseDomainEntity
    {
        public T Object { get; set; }
        public string? ReportText { get; set; }
        public bool IsComplete { get; set; }
        public User TargetUser { get; set; }
        public int TargetUserId { get; set; }
    }
}
