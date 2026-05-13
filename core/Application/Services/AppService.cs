using Domain.Services;
using Domain.Stores;

namespace Application.Services
{
    public class AppService<T> : IAppService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public AppService(IRepository<T> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<T> CreateAsync(T model)
        {
            return await _repository.CreateAsync(model);
        }

        public async Task DeleteAsync(T model)
        {
            await _repository.DeleteAsync(model);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var model = await _repository.GetByIdAsync(id);
            if(model == null)
            {
                throw new ApplicationException(nameof(T) + " Not found");
            }

            await _repository.DeleteAsync(model);
        }

        public async Task<IEnumerable<T>> GetAllAsync(string filter = null)
        {
            return await _repository.GetAllAsync(filter);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> UpdateAsync(T model)
        {
            return await _repository.UpdateAsync(model);
        }
    }
}
