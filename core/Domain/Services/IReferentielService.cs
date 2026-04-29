using Domain.Models;

namespace Domain.Services
{
    public interface IReferentielService
    {
        Task<IEnumerable<RefTypePlan>> GetAllTypePlansAsync();
        Task<IEnumerable<RefTypePhoto>> GetAllTypePhotosAsync();
        Task<IEnumerable<RefTypeMouvement>> GetAllTypeMouvementsAsync();
        Task<IEnumerable<RefTypeIntervention>> GetAllTypeInterventionsAsync();
        Task<IEnumerable<RefTypeContrat>> GetAllTypeContratsAsync();
        Task<IEnumerable<RefStatutIntervention>> GetAllStatutInterventionsAsync();
        Task<IEnumerable<RefStatutFacture>> GetAllStatutFacturesAsync();
        Task<IEnumerable<RefRole>> GetAllRolesAsync();
    }
}
