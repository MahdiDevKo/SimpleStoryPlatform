using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Services
{
    public interface IReviewReportRepository : IGenericRepository<StoryReviewReport>
    {
        Task<List<StoryReviewReport>> GetAllWithDetail();
        Task<StoryReviewReport?> GetReportWithDetails(Guid reportGuid);

        Task DeleteSameReportsAsync(int reviewId);
    }
}
