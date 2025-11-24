using Microsoft.EntityFrameworkCore;
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
    }
}
