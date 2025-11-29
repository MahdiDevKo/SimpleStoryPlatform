using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDomainEntity
    {
        private readonly StoryPlatformDbContext _context;
        public GenericRepository(StoryPlatformDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<bool> DeleteAsync(Guid publicId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(Guid publicId)
        {
            //var res = await _context.Set<T>().Where(e => e.PublicId == publicId);
            //return 
            throw new NotImplementedException();
        }

        public async Task<T?> GetAsync(int id)
        {
            T? res = await _context.Set<T>().FindAsync(id);
            return res;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByGuidAsync(Guid publicId)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.PublicId == publicId);
        }

        public async Task<int> GetIdByGuid(Guid publicId)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.PublicId == publicId);

            return entity.Id;
        }

        public async Task<T> UpdateEntityAsync(T entity)
        {
            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        //writed by chatGPT
        public async Task<T> UpdateStatesAsync(T entity)
        {
            // Attach the entity to the context if it's not already tracked
            var trackedEntity = _context.Set<T>().Local.FirstOrDefault(e => e.Id == entity.Id);
            if (trackedEntity == null)
            {
                _context.Set<T>().Attach(entity);
            }

            // Mark the entity as modified so EF will save all changes
            _context.Entry(entity).State = EntityState.Modified;

            // Save changes
            await _context.SaveChangesAsync();

            return entity;
        }


        public async Task<PageResponse<T>> GetPageAsync(BaseRequest request, IQueryable<T>? query = null)
        {
            var response = new PageResponse<T>()
            {
                //TotalItems = await _context.Set<T>().CountAsync(),
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber
            };

            if (query == null)
                query = _context.Set<T>();

            response.TotalItems = await query.CountAsync();

            response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)response.PageSize);

            response.Items = await query
                .Skip(response.PageSize * (response.CurrentPage - 1))
                .Take(response.PageSize)
                .ToListAsync();

            return response;
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }

        /*
   ===== Helper =====
   GetPageAsync Method

   This method is a generic helper for paging a list of items of type T.

   Parameters:
   - BaseRequest request: Contains paging parameters.
       - PageNumber: The page to retrieve (1-based index).
       - PageSize: Number of items per page (default is 10).

   - IQueryable<T>? query (optional): 
       If the application layer wants to apply filters (e.g., search options) or use specific Include statements 
       (for related entities), they can pass a preconfigured IQueryable<T>. 
       If null, the method will query all items from the corresponding DbSet<T>.

   Behavior:
   1. Calculates TotalItems (total number of items in the dataset or filtered query).
   2. Calculates TotalPages based on TotalItems and PageSize.
   3. Retrieves only the items for the requested page using Skip and Take.
   4. Returns a PageResponse<T> object containing:
       - Items: The list of items for the current page.
       - CurrentPage: The current page number.
       - PageSize: Number of items per page.
       - TotalItems: Total number of items matching the query.
       - TotalPages: Total number of pages.

   Usage:
   - For simple paging, just pass a BaseRequest with the desired PageNumber.
   - For filtered or more complex queries (e.g., search by name or include related entities), 
     prepare the IQueryable<T> in the application layer and pass it to this method.
*/

    }
}
