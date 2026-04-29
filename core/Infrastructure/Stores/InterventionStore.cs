using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Stores
{
    public class InterventionStore : IInterventionStore
    {
        private readonly ExsaDbContext _dbContext;

        public InterventionStore(ExsaDbContext exsaDbContext) 
        {
            _dbContext = exsaDbContext ?? throw new ApplicationException(nameof(exsaDbContext));
        }

        public async Task<AffectationIntervention> AddAffectationInterventionAsync(AffectationIntervention affectationIntervention)
        {
            var entity = affectationIntervention.ToEntity();
            _dbContext.AFFECTATION_INTERVENTIONs.Add(entity);
            await _dbContext.SaveChangesAsync();

            return affectationIntervention;
        }

        public async Task<IEnumerable<AffectationIntervention>> GetAffectationsAsync()
        {
            return (await _dbContext.AFFECTATION_INTERVENTIONs.ToListAsync()).ToModelCollection();
        }

        public async Task RemoveAffectationAsync(AffectationIntervention affectation)
        {
            var entity = affectation.ToEntity();
            _dbContext.AFFECTATION_INTERVENTIONs.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Intervention> CreateAsync(Intervention model)
        {
            var entity = model.ToEntity();
            _dbContext.INTERVENTIONs.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.ToModel();
        }

        public async Task DeleteAsync(Intervention model)
        {
            var entity = model.ToEntity();
            _dbContext.INTERVENTIONs.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Intervention>> GetAllAsync()
        {
            return (await _dbContext.INTERVENTIONs.ToListAsync()).ToModelCollection();
        }

        public async Task<Intervention> GetByIdAsync(Guid id)
        {
            return (await _dbContext.INTERVENTIONs.AsNoTracking().FirstAsync(x => x.ID_INTERVENTION == id)).ToModel();
        }

        public async Task<Intervention> UpdateAsync(Intervention model)
        {
            var entity = model.ToEntity();
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity.ToModel();
        }
    }
}
