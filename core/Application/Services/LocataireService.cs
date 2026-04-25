using Domain.Models;
using Domain.Services;
using Domain.Stores;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace Application.Services
{
    public class LocataireService: IGenericService<Locataire>
    {
        private readonly ILocataireStore _locataireStore;
        public LocataireService(ILocataireStore locataire) 
        {
            _locataireStore = locataire ?? throw new ApplicationException(nameof(locataire));
        }

        public Task<bool> AnyAsync(Expression<Func<Locataire, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Locataire> CreateAsync(Locataire entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Locataire>> FindAsync(Expression<Func<Locataire, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Locataire>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return (await _locataireStore.GetAllLocataireAsync()).ToImmutableList();
        }

        public async Task<Locataire?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _locataireStore.GetLocataireByIdAsync(id);
        }

        public Task UpdateAsync(Locataire entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
