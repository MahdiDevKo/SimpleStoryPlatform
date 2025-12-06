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
    public interface IGenericRepository<T> where T : BaseDomainEntity
    {
        Task<T?> GetAsync(int id);
        IQueryable<T> GetQueryable();
        Task<T> AddAsync(T entity);
        Task<bool> DeleteAsync(Guid publicId);
        Task<T?> GetByGuidAsync(Guid? publicId);
        Task<IEnumerable<T>> GetAllAsync();
        Task<PageResponse<T>> GetPageAsync(BaseRequest request, IQueryable<T>? query = null);
        Task<T> UpdateEntityAsync(T entity);
        Task<T> UpdateStatesAsync(T entity);
        Task<bool> ExistsAsync(Guid publicId);
        Task<int> GetIdByGuid(Guid? publicId);
        Task SaveAsync(T entity);
    }
}
