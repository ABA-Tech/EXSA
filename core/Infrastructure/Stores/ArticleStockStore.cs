using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Stores
{
    public class ArticleStockStore : IArticleStockStore
    {
        private readonly ExsaDbContext _context;

        public ArticleStockStore(ExsaDbContext exsaDbContext)
        {
            _context = exsaDbContext ?? throw new ApplicationException(nameof(exsaDbContext));
        }

        public async Task<ArticleStock> CreateAsync(ArticleStock model)
        {
            var entity = model.ToEntity();
            _context.ARTICLE_STOCKs.Add(entity);
            await _context.SaveChangesAsync();

            return entity.ToModel();
        }

        public async Task DeleteAsync(ArticleStock model)
        {
            var entity = model.ToEntity();
            _context.ARTICLE_STOCKs.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ArticleStock>> GetAllAsync(string filter = null)
        {
            return (await _context.ARTICLE_STOCKs.ToListAsync()).ToModelCollection();
        }

        public async Task<ArticleStock> GetByIdAsync(Guid id)
        {
            return (await _context.ARTICLE_STOCKs.AsNoTracking().FirstOrDefaultAsync(x=>x.ID_ARTICLE == id))?.ToModel();
        }

        public async Task<ArticleStock> UpdateAsync(ArticleStock model)
        {
            var entity = model.ToEntity();
            _context.ARTICLE_STOCKs.Update(entity);
            await _context.SaveChangesAsync();

            return entity.ToModel();
        }
    }
}
