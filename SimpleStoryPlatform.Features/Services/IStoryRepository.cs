using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Services
{
    public interface IStoryRepository : IGenericRepository<Story>
    {
        Task<List<Story>> GetWritedStories(Guid userGuid);
        Task<Story> GetStoryDetails(Guid storyGuid);
        Task<List<Story>?> SearchStories(string? searchValue, bool isAdmin = false);
        Task<string?> AddStoryReview(StoryReview review);


    }
}
