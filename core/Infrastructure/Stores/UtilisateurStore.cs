using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Stores
{
    public class UtilisateurStore : IRepository<Utilisateur>
    {
        private readonly ExsaDbContext _context;

        public UtilisateurStore(ExsaDbContext context)
        {
            _context = context ?? throw new ApplicationException(nameof(context));
        }
        public async Task<Utilisateur> CreateAsync(Utilisateur model)
        {
            var entity = model.ToEntity();
            _context.UTILISATEURs.Add(entity);
            var res = await _context.SaveChangesAsync();
            return entity.ToModel();
        }

        public async Task DeleteAsync(Utilisateur model)
        {
            var entity = model.ToEntity();
            _context.UTILISATEURs.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Utilisateur>> GetAllAsync(string filter = null)
        {
            return (await _context.UTILISATEURs.ToListAsync()).ToModelCollection();
        }

        public async Task<Utilisateur> GetByIdAsync(Guid id)
        {
            return (await _context.UTILISATEURs.FirstAsync(x => x.ID_UTILISATEUR == id)).ToModel();
        }

        public async Task<Utilisateur> UpdateAsync(Utilisateur model)
        {
            var entity = model.ToEntity();
            _context.UTILISATEURs.Update(entity);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
