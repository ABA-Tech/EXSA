using Domain.Models;

namespace Domain.Stores
{
    public interface IFactureStore : IRepository<Facture>
    {
        Task<IEnumerable<Facture>> GetByInterventionAsync(Guid idIntervention);
        Task<Facture> GetWithDetailsAsync(Guid idFacture);
        Task<Reglement> AddReglementAsync(Reglement reglement);
        Task<IEnumerable<Reglement>> GetReglementsAsync(Guid idFacture);
        Task<string> GenerateReferenceAsync(Guid idLocataire);
    }
}