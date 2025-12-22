using Azure;
using Microsoft.EntityFrameworkCore;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Infrastructure.DbSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Infrastructure.Services.Repositories
{
    public class StoryPlayListRepository : GenericRepository<StoryPlayList>, IStoryPlayListRepository
    {
        private readonly StoryPlatformDbContext _context;
        public StoryPlayListRepository(StoryPlatformDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PageResponse<Story>> GetPageAsync(int playlistId, BaseRequest req)
        {
            var playlist = await _context.StoryPlayLists
                .Include(pl => pl.Stories)
                .FirstOrDefaultAsync(pl => pl.Id == playlistId);

            var res = new PageResponse<Story>()
            {
                PageSize = req.PageSize,
                TotalItems = playlist?.Stories.Count ?? 0,
                CurrentPage = req.PageNumber,

                Items = playlist?.Stories
                .Skip((req.PageNumber - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList()
            };

            res.TotalPages = (int)Math.Ceiling(res.TotalItems / (double)res.PageSize);

            return res;
        }
    }
}
