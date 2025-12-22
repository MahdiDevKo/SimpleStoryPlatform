using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Services
{
    public interface IStoryPlayListRepository : IGenericRepository<StoryPlayList>
    {
        Task<PageResponse<Story>> GetPageAsync(int playlistId, BaseRequest req);
    }
}
