using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Services
{
    public interface IStoryReleaseRepository : IGenericRepository<StoryReleaseRequest>
    {
        Task<StoryReleaseRequest?> GetReportWithDetails(Guid reportGuid);
    }
}
