using Domain.Models;
using Domain.Services;
using Domain.Stores;

namespace Application.Services
{
    public class FactureService : AppService<Facture>, IFactureService
    {
        private readonly IFactureStore _factureStore;

        public FactureService(IFactureStore factureStore) : base(factureStore)
        {
            _factureStore = factureStore;
        }

        public async Task<IEnumerable<Facture>> GetByInterventionAsync(Guid idIntervention)
        {
            return await _factureStore.GetByInterventionAsync(idIntervention);
        }

        public async Task<Facture> GetWithDetailsAsync(Guid idFacture)
        {
            return await _factureStore.GetWithDetailsAsync(idFacture);
        }

        public async Task<Facture> CreateForInterventionAsync(
            Guid idIntervention,
            string nomClient,
            Guid idLocataire,
            decimal montantHt)
        {
            var tauxTva = 19.25m;
            var totalXaf = Math.Round(montantHt * (1 + tauxTva / 100), 2);
            var reference = await _factureStore.GenerateReferenceAsync(idLocataire);

            var facture = new Facture
            {
                IdFacture = Guid.NewGuid(),
                IdLocataire = idLocataire,
                IdIntervention = idIntervention,
                Reference = reference,
                NomClient = nomClient,
                Statut = "EMISE",
                SousTotalXaf = montantHt,
                TauxTva = tauxTva,
                TotalXaf = totalXaf,
                DateCreation = DateTime.Now,
            };

            return await _factureStore.CreateAsync(facture);
        }

        public async Task<Reglement> AddReglementAsync(Guid idFacture, Reglement reglement)
        {
            var facture = await _factureStore.GetByIdAsync(idFacture);
            if (facture == null)
                throw new ApplicationException("Facture introuvable");

            reglement.IdFacture = idFacture;
            reglement.IdReglement = Guid.NewGuid();
            reglement.DateCreation = DateTime.Now;
            reglement.DateModification = DateTime.Now;

            return await _factureStore.AddReglementAsync(reglement);
        }

        public async Task<IEnumerable<Reglement>> GetReglementsAsync(Guid idFacture)
        {
            return await _factureStore.GetReglementsAsync(idFacture);
        }
    }
}