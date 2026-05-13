using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Stores
{
    public class MouvementStockStore : IMouvementStockStore
    {
        private readonly ExsaDbContext _context;

        public MouvementStockStore(ExsaDbContext exsaDbContext)
        {
            _context = exsaDbContext ?? throw new ApplicationException(nameof(exsaDbContext));
        }

        public async Task<MouvementStock> CreateAsync(MouvementStock model)
        {
            var entity = model.ToEntity();
            _context.MOUVEMENT_STOCKs.Add(entity);
            await _context.SaveChangesAsync();

            return entity.ToModel();
        }

        public async Task DeleteAsync(MouvementStock model)
        {
            var entity = model.ToEntity();
            _context.MOUVEMENT_STOCKs.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MouvementStock>> GetAllAsync(string filter = null)
        {
            return (await _context.MOUVEMENT_STOCKs.Include(x=>x.ID_ARTICLENavigation).Include(x=>x.ID_INTERVENTIONNavigation).Where(x=>filter == null || x.ID_ARTICLE.ToString() == filter).ToListAsync()).ToModelCollection();
        }

        public async Task<MouvementStock> GetByIdAsync(Guid id)
        {
            return (await _context.MOUVEMENT_STOCKs.FirstOrDefaultAsync(x=>x.ID_ARTICLE == id))?.ToModel();
        }

        public async Task<MouvementStock> UpdateAsync(MouvementStock model)
        {
            var entity = model.ToEntity();
            _context.MOUVEMENT_STOCKs.Update(entity);
            await _context.SaveChangesAsync();

            return entity.ToModel();
        }
    }
}
