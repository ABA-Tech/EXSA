using Domain.Models;

namespace Domain.Stores
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync(string filter = null);
        public Task<T> GetByIdAsync(Guid id);
        public Task<T> CreateAsync(T model);
        public Task<T> UpdateAsync(T model);
        public Task DeleteAsync(T model);
        public Task DeleteByIdAsync(Guid id);
    }
}
