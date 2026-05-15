using Domain.Models;

namespace Domain.Stores
{
    public interface IReferentielStore
    {
        Task<IEnumerable<RefTypePlan>> GetTypePlansAsync();
        Task<IEnumerable<RefTypePhoto>> GetTypePhotosAsync();
        Task<IEnumerable<RefTypeMouvement>> GetTypeMouvementsAsync();
        Task<IEnumerable<RefTypeIntervention>> GetInterventionsAsync();
        Task<IEnumerable<RefTypeContrat>> GetTypeContratsAsync();
        Task<IEnumerable<RefStatutIntervention>> GetStatutInterventionsAsync();
        Task<IEnumerable<RefStatutFacture>> GetStatutFacturesAsync();
        Task<IEnumerable<RefRole>> GetRolesAsync();
        Task<IEnumerable<RefTypeeDepenseIntervention>> GetTypeDepenseInterventionsAsync();

        Task<IEnumerable<RefTypeVehicule>> GetTypeVehiculesAsync();
        Task<IEnumerable<RefStatutVehicule>> GetStatusVehiculeAsync();
        Task<IEnumerable<RefTypeDepenseVehicule>> GetTypeDepenseVehiculeAsync();
    }
}
