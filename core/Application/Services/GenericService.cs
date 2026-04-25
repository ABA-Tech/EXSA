using Domain.Services;
using Domain.Stores;
using System.Linq.Expressions;

namespace Application.Services
{

    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return await _repository.FindAsync(predicate, cancellationToken);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return await _repository.AnyAsync(predicate, cancellationToken);
        }

        public async Task<int> CountAsync(
            CancellationToken cancellationToken = default)
        {
            return await _repository.CountAsync(cancellationToken);
        }

        public async Task<T> CreateAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {

            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
                return false;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
