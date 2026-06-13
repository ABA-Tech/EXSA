using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Stores
{
    public class FactureStore : IFactureStore
    {
        private readonly ExsaDbContext _dbContext;

        public FactureStore(ExsaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Facture> CreateAsync(Facture model)
        {
            var entity = model.ToEntity();
            _dbContext.FACTUREs.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ToModel();
        }

        public async Task<IEnumerable<Facture>> GetAllAsync(string filter = null)
        {
            var query = _dbContext.FACTUREs
                .Include(f => f.STATUTNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter))
                query = query.Where(f => f.STATUT == filter);

            return (await query.ToListAsync()).Select(f => f.ToModel());
        }

        public async Task<Facture> GetByIdAsync(Guid id)
        {
            var entity = await _dbContext.FACTUREs
                .Include(f => f.STATUTNavigation)
                .FirstOrDefaultAsync(f => f.ID_FACTURE == id);

            if (entity == null)
                throw new ApplicationException("Facture introuvable");

            return entity.ToModel();
        }

        public async Task<Facture> GetWithDetailsAsync(Guid idFacture)
        {
            var entity = await _dbContext.FACTUREs
                .Include(f => f.STATUTNavigation)
                .Include(f => f.LIGNE_FACTUREs)
                .Include(f => f.REGLEMENTs)
                .FirstOrDefaultAsync(f => f.ID_FACTURE == idFacture);

            if (entity == null)
                throw new ApplicationException("Facture introuvable");

            return entity.ToModelWithDetails();
        }
        public async Task<IEnumerable<Facture>> GetByInterventionAsync(Guid idIntervention)
        {
            var factures = await _dbContext.FACTUREs
                .Include(f => f.STATUTNavigation)
                .Include(f => f.REGLEMENTs)
                .Where(f => f.ID_INTERVENTION == idIntervention)
                .Select(f => f.ToModelWithDetails())
                .ToListAsync();

            if (factures.Count > 0)
                return factures;

            var intervention = await _dbContext.INTERVENTIONs
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.ID_INTERVENTION == idIntervention);

            if (intervention == null ||
                !intervention.MONTANT_CONVENU_XAF.HasValue ||
                intervention.MONTANT_CONVENU_XAF <= 0)
            {
                return Enumerable.Empty<Facture>();
            }

            const decimal tauxTva = 19.25m;

            var facture = new Facture
            {
                IdFacture = Guid.NewGuid(),
                IdLocataire = intervention.ID_LOCATAIRE,
                IdIntervention = intervention.ID_INTERVENTION,
                Reference = await GenerateReferenceAsync(intervention.ID_LOCATAIRE),
                NomClient = intervention.NOM_CLIENT ?? string.Empty,
                Statut = "EMISE",
                SousTotalXaf = intervention.MONTANT_CONVENU_XAF.Value,
                TauxTva = tauxTva,
                TotalXaf = Math.Round(
                    intervention.MONTANT_CONVENU_XAF.Value * (1 + tauxTva / 100m),
                    2,
                    MidpointRounding.AwayFromZero),
                DateCreation = DateTime.UtcNow
            };

            try
            {
                await CreateAsync(facture);
            }
            catch (DbUpdateException)
            {
                // Une autre requête a probablement créé la facture
                // entre notre lecture et l'insertion.
            }

            return await _dbContext.FACTUREs
                .Include(f => f.STATUTNavigation)
                .Include(f => f.REGLEMENTs)
                .Where(f => f.ID_INTERVENTION == idIntervention)
                .Select(f => f.ToModelWithDetails())
                .ToListAsync();
        }

        public async Task<Facture> UpdateAsync(Facture model)
        {
            var entity = model.ToEntity();
            _dbContext.FACTUREs.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ToModel();
        }

        public async Task DeleteAsync(Facture model)
        {
            var entity = model.ToEntity();
            _dbContext.FACTUREs.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Reglement> AddReglementAsync(Reglement reglement)
        {
            var entity = reglement.ToEntity();
            _dbContext.REGLEMENTs.Add(entity);
            await _dbContext.SaveChangesAsync();

            // Mettre à jour le statut de la facture
            await UpdateStatutFactureAsync(reglement.IdFacture);

            return entity.ToModel();
        }

        public async Task<IEnumerable<Reglement>> GetReglementsAsync(Guid idFacture)
        {
            return (await _dbContext.REGLEMENTs
                .Where(r => r.ID_FACTURE == idFacture)
                .OrderByDescending(r => r.DATE_REGLEMENT)
                .ToListAsync())
                .Select(r => r.ToModel());
        }

        public async Task<string> GenerateReferenceAsync(Guid idLocataire)
        {
            var annee = DateTime.Now.Year;
            var count = await _dbContext.FACTUREs
                .Where(f => f.ID_LOCATAIRE == idLocataire && f.DATE_CREATION.Year == annee)
                .CountAsync();
            return $"FAC-{annee}-{(count + 1):D3}";
        }

        // --- Méthode privée ---

        private async Task UpdateStatutFactureAsync(Guid idFacture)
        {
            var factureInfo = await _dbContext.FACTUREs
                .Where(f => f.ID_FACTURE == idFacture)
                .Select(f => new
                {
                    f.ID_FACTURE,
                    f.SOUS_TOTAL_XAF, // On l'utilise en attendant la confirmation de l'utilisation de la TVA
                    TotalRegle = f.REGLEMENTs.Sum(r => r.MONTANT_XAF)
                })
                .FirstOrDefaultAsync();

            if (factureInfo == null) return;

            var statut = factureInfo.TotalRegle >= factureInfo.SOUS_TOTAL_XAF
                ? "PAYEE"
                : factureInfo.TotalRegle > 0
                    ? "PAYEE_PARTIELLEMENT"
                    : "ENVOYEE";

            await _dbContext.FACTUREs
                .Where(f => f.ID_FACTURE == idFacture)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(f => f.STATUT, statut)
                    .SetProperty(f => f.DATE_MODIFICATION, DateTime.Now));
        }
    }
}