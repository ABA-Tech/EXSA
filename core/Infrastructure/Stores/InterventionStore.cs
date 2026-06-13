using Domain.Models;
using Domain.Models.Outputs;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
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
            return (await _dbContext.AFFECTATION_INTERVENTIONs.Include(x=>x.ID_TECHNICIENNavigation).Where(x=>x.ID_INTERVENTION == IdIntervention).ToListAsync()).ToModelCollection();
        }

        public async Task RemoveAffectationAsync(AffectationIntervention affectation)
        {
            var entity = affectation.ToEntity();
            _dbContext.AFFECTATION_INTERVENTIONs.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Intervention> CreateAsync(Intervention model)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // 1. Créer l'intervention
                var entity = model.ToEntity();
                _dbContext.INTERVENTIONs.Add(entity);
                await _dbContext.SaveChangesAsync();

                // 2. Si un montant convenu est fourni, créer la facture + ligne automatiquement
                if (model.MontantConvenuXaf.HasValue && model.MontantConvenuXaf > 0)
                {
                    var tauxTva = 19.25m;
                    var sousTotalXaf = model.MontantConvenuXaf.Value;
                    var totalXaf = Math.Round(sousTotalXaf * (1 + tauxTva / 100), 2);

                    // Générer la référence FAC-YYYY-NNN
                    var annee = DateTime.Now.Year;
                    var count = await _dbContext.FACTUREs
                        .Where(f => f.ID_LOCATAIRE == model.IdLocataire && f.DATE_CREATION.Year == annee)
                        .CountAsync();
                    var reference = $"FAC-{annee}-{(count + 1):D3}";

                    var facture = new FACTURE
                    {
                        ID_FACTURE = Guid.NewGuid(),
                        ID_LOCATAIRE = model.IdLocataire,
                        ID_INTERVENTION = entity.ID_INTERVENTION,
                        REFERENCE = reference,
                        NOM_CLIENT = model.NomClient ?? string.Empty,
                        STATUT = "BROUILLON",
                        SOUS_TOTAL_XAF = sousTotalXaf,
                        TAUX_TVA = tauxTva,
                        TOTAL_XAF = totalXaf,
                        DATE_CREATION = DateTime.Now,
                        DATE_MODIFICATION = DateTime.Now,
                    };

                    _dbContext.FACTUREs.Add(facture);

                    //var ligneFacture = new LIGNE_FACTURE
                    //{
                    //    ID_LIGNE = Guid.NewGuid(),
                    //    ID_FACTURE = facture.ID_FACTURE,
                    //    DESCRIPTION = model.Titre,
                    //    QUANTITE = 1,
                    //    PRIX_UNITAIRE = sousTotalXaf,
                    //    TOTAL_XAF = sousTotalXaf,
                    //};

                    //_dbContext.LIGNE_FACTUREs.Add(ligneFacture);
                    await _dbContext.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return entity.ToModel();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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

        public async Task<IEnumerable<Intervention>> GetAllAsync(string status = null)
        {
            return (await _dbContext.INTERVENTIONs.Include(x=>x.ID_LOCATAIRENavigation).Include(x=>x.STATUTNavigation).Where(x=>status == null || x.STATUT == status).ToListAsync()).ToModelCollection();
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

            // Modification de la facture
            var facture = _dbContext.FACTUREs.FirstOrDefault(x => x.ID_INTERVENTION == model.IdIntervention);
            if (facture != null && model.MontantConvenuXaf.HasValue)
            {

                var tauxTva = 19.25m;
                var sousTotalXaf = model.MontantConvenuXaf.Value;
                var totalXaf = Math.Round(sousTotalXaf * (1 + tauxTva / 100), 2);

                facture.NOM_CLIENT = model.NomClient ?? string.Empty;
                facture.TOTAL_XAF = totalXaf;
                facture.SOUS_TOTAL_XAF = sousTotalXaf;
            }
            else if(model.MontantConvenuXaf.HasValue && model.MontantConvenuXaf > 0)
            {
                // 2. Si un montant convenu est fourni, créer la facture + ligne automatiquement
                var tauxTva = 19.25m;
                var sousTotalXaf = model.MontantConvenuXaf.Value;
                var totalXaf = Math.Round(sousTotalXaf * (1 + tauxTva / 100), 2);

                // Générer la référence FAC-YYYY-NNN
                var annee = DateTime.Now.Year;
                var count = await _dbContext.FACTUREs
                    .Where(f => f.ID_LOCATAIRE == model.IdLocataire && f.DATE_CREATION.Year == annee)
                    .CountAsync();
                var reference = $"FAC-{annee}-{(count + 1):D3}";

                var factureNew = new FACTURE
                {
                    ID_FACTURE = Guid.NewGuid(),
                    ID_LOCATAIRE = model.IdLocataire,
                    ID_INTERVENTION = entity.ID_INTERVENTION,
                    REFERENCE = reference,
                    NOM_CLIENT = model.NomClient ?? string.Empty,
                    STATUT = "BROUILLON",
                    SOUS_TOTAL_XAF = sousTotalXaf,
                    TAUX_TVA = tauxTva,
                    TOTAL_XAF = totalXaf,
                    DATE_CREATION = DateTime.Now,
                    DATE_MODIFICATION = DateTime.Now                        
                };

                _dbContext.FACTUREs.Add(factureNew);

                //var ligneFacture = new LIGNE_FACTURE
                //{
                //    ID_LIGNE = Guid.NewGuid(),
                //    ID_FACTURE = factureNew.ID_FACTURE,
                //    DESCRIPTION = model.Titre,
                //    QUANTITE = 1,
                //    PRIX_UNITAIRE = sousTotalXaf,
                //    TOTAL_XAF = sousTotalXaf,
                //};

                //_dbContext.LIGNE_FACTUREs.Add(ligneFacture);
                await _dbContext.SaveChangesAsync();
            }

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

        public async Task<bool> UpdateDepenseRationTransport(DepenseIntervention depenseIntervention)
        {
            var depense = await _dbContext.DEPENSE_INTERVENTIONs.AsNoTracking().FirstOrDefaultAsync(x => x.ID_DEPENSE == depenseIntervention.IdDepense);
            if (depense == null)
            {
                return false;
            }

            var employe = await _dbContext.EMPLOYEs.FirstOrDefaultAsync(x => x.ID_EMPLOYE == depenseIntervention.IdEmploye);
            if (employe == null)
                return false;

            depense.MONTANT_XAF = depenseIntervention.Montant;
            depense.ID_SAISIE_PAR = depenseIntervention.SaisiPar;

            _dbContext.DEPENSE_INTERVENTIONs.Update(depense);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<int> AddDepenseIntervention(DepenseIntervention saisieDepenseIntervention, bool isUpdate = false)
        {
            var employe = await _dbContext.EMPLOYEs.FirstOrDefaultAsync(x=> (!isUpdate && x.ID_UTILISATEUR == saisieDepenseIntervention.IdEmploye) || x.ID_EMPLOYE == saisieDepenseIntervention.IdEmploye);
            if (employe == null)
                return 0;

            var ligneExists = await _dbContext.DEPENSE_INTERVENTIONs
                        .Where(x => (x.ID_EMPLOYE == employe.ID_EMPLOYE && x.TYPE_DEPENSE == saisieDepenseIntervention.TypeDepense && x.DATE_DEPENSE == saisieDepenseIntervention.DateDepense)
                                || (x.REFERENCE == saisieDepenseIntervention.Reference && saisieDepenseIntervention.TypeDepense == x.TYPE_DEPENSE && x.ID_EMPLOYE == employe.ID_EMPLOYE && x.DATE_DEPENSE == saisieDepenseIntervention.DateDepense))
                        .ToListAsync();
            if (ligneExists.Any())
                throw new ApplicationException("une saisie similaire existe déjà");

            var entity = new DEPENSE_INTERVENTION
            {
                DATE_CREATION = saisieDepenseIntervention.DateCreation,
                DATE_DEPENSE = saisieDepenseIntervention.DateDepense,
                ID_EMPLOYE = employe.ID_EMPLOYE,
                ID_INTERVENTION = saisieDepenseIntervention.IdIntervention,
                ID_SAISIE_PAR = saisieDepenseIntervention.SaisiPar,
                NOTE = saisieDepenseIntervention.Note,
                REFERENCE = saisieDepenseIntervention.Reference,
                TYPE_DEPENSE = saisieDepenseIntervention.TypeDepense,
                MONTANT_XAF = saisieDepenseIntervention.Montant,
            };

            _dbContext.DEPENSE_INTERVENTIONs.Add(entity);
            var res = await _dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<IEnumerable<RationTransportOutput>> GetSaisiesRationTransportAsync(Guid idIntervention)
        {
            var result = await _dbContext.RationTransportOutputs
                .FromSqlRaw("EXEC GET_RATION_TRANSPORT_INTERVENTION @p_ID_INTERVENTION",
                    new SqlParameter("@p_ID_INTERVENTION", idIntervention))
                .ToListAsync();
            return result;
        }

        public async Task<DepenseIntervention?> GetDepenseRationTransportById(Guid idDepense)
        {
            var depenseIntervention = await _dbContext.DEPENSE_INTERVENTIONs.AsNoTracking().FirstOrDefaultAsync(x=>x.ID_DEPENSE == idDepense);

            return depenseIntervention == null ? null : new DepenseIntervention
            {
                DateDepense = depenseIntervention.DATE_DEPENSE,
                IdEmploye = depenseIntervention.ID_EMPLOYE,
                Reference = depenseIntervention.REFERENCE,
                TypeDepense = depenseIntervention.TYPE_DEPENSE,
                DateCreation = depenseIntervention.DATE_CREATION,
                IdIntervention = depenseIntervention.ID_INTERVENTION,
                Montant = depenseIntervention.MONTANT_XAF,
                Note = depenseIntervention.NOTE,
                SaisiPar = depenseIntervention.ID_SAISIE_PAR,
                IdDepense = depenseIntervention.ID_DEPENSE
            };
        }
    }
}
