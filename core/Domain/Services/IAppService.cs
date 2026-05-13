
namespace Domain.Services
{
    public interface IAppService<T> where T : class
    {
       Task<IEnumerable<T>> GetAllAsync(string filter = null);
       Task<T> GetByIdAsync(Guid id);
       Task<T> CreateAsync(T model);
       Task<T> UpdateAsync(T model);
       Task DeleteAsync(T model);
       Task DeleteByIdAsync(Guid id);
    }
}
