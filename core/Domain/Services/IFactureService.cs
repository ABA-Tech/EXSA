using Domain.Models;

namespace Domain.Services
{
    public interface IFactureService : IAppService<Facture>
    {
        Task<IEnumerable<Facture>> GetByInterventionAsync(Guid idIntervention);
        Task<Facture> GetWithDetailsAsync(Guid idFacture);
        Task<Facture> CreateForInterventionAsync(Guid idIntervention, string nomClient, Guid idLocataire, decimal montantHt);
        Task<Reglement> AddReglementAsync(Guid idFacture, Reglement reglement);
        Task<IEnumerable<Reglement>> GetReglementsAsync(Guid idFacture);
    }
}