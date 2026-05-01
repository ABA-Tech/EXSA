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
            if (_dbContext.AFFECTATION_INTERVENTIONs.FirstOrDefault(x => x.ID_INTERVENTION == affectationIntervention.IdIntervention && x.ID_TECHNICIEN == affectationIntervention.IdTechnicien) != null)
                return affectationIntervention;

            _dbContext.AFFECTATION_INTERVENTIONs.Add(entity);
            await _dbContext.SaveChangesAsync();

            return affectationIntervention;
        }

        public async Task<IEnumerable<AffectationIntervention>> GetAffectationsAsync(Guid IdIntervention)
        {
            return (await _dbContext.AFFECTATION_INTERVENTIONs.Where(x=>x.ID_INTERVENTION == IdIntervention).ToListAsync()).ToModelCollection();
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
            return (await _dbContext.INTERVENTIONs.Include(x=>x.STATUTNavigation).ToListAsync()).ToModelCollection();
        }

        public async Task<Intervention> GetByIdAsync(Guid id)
        {
            return (await _dbContext.INTERVENTIONs.Include(x=>x.STATUTNavigation).AsNoTracking().FirstAsync(x => x.ID_INTERVENTION == id)).ToModel();
        }

        public async Task<Intervention> UpdateAsync(Intervention model)
        {
            var entity = model.ToEntity();
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity.ToModel();
        }

        public async Task<IEnumerable<PhotoIntervention>> GetPhotoInterventionAsync(Guid idIntervention)
        {
            return (await _dbContext.PHOTO_INTERVENTIONs.Include(x => x.ID_UPLOADEURNavigation).Where(x=>x.ID_INTERVENTION==idIntervention).ToListAsync()).ToModelCollection();
        }

        public async Task DeletePhotoInterventionAsync(PhotoIntervention photoIntervention)
        {
            var entity = photoIntervention.ToEntity();
            _dbContext.PHOTO_INTERVENTIONs.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UploadPhotoInterventionAsync(PhotoIntervention intervention)
        {
            var entity = intervention.ToEntity();
            _dbContext.PHOTO_INTERVENTIONs.Add(entity);

            var res = await _dbContext.SaveChangesAsync();
            return res > 0;
        }

        public async Task<PhotoIntervention?> GetPhotoInterventionByIdAsync(Guid idPhotoIntervention)
        {
            return (await _dbContext.PHOTO_INTERVENTIONs.Include(x=>x.ID_UPLOADEURNavigation).AsNoTracking().FirstOrDefaultAsync(x => x.ID_PHOTO == idPhotoIntervention))?.ToModel();
        }
    }
}
