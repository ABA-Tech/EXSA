using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Stores
{
    public class LocataireStore : ILocataireStore
    {
        private readonly ExsaDbContext _exsaDbContext;
        public LocataireStore(ExsaDbContext context)
        {
            _exsaDbContext = context ?? throw new ApplicationException(nameof(context));
        }
        public Task<Locataire> CreateLocataireAsync(Locataire Locataire)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLocataireAsync(Locataire locataire)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLocataireByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Locataire>> GetAllLocataireAsync()
        {
            return (await _exsaDbContext.LOCATAIREs.ToListAsync()).ToModelCollection();
        }

        public async Task<Locataire> GetLocataireByIdAsync(Guid id)
        {
            return (await _exsaDbContext.LOCATAIREs.FirstAsync(x=>x.ID_LOCATAIRE == id)).ToModel();
        }

        public Task<Locataire> UpdateLocataireAsync(Locataire Locataire)
        {
            throw new NotImplementedException();
        }
    }
}
