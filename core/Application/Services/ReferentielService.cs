using Domain.Models;
using Domain.Services;
using Domain.Stores;

namespace Application.Services
{
    public class ReferentielService : IReferentielService
    {
        private readonly IReferentielStore _referentielStore;

        public ReferentielService(IReferentielStore referentielStore)
        {
            _referentielStore = referentielStore ?? throw new ArgumentNullException(nameof(referentielStore));
        }
        public async Task<IEnumerable<RefTypeIntervention>> GetAllTypeInterventionsAsync()
        {
            return await _referentielStore.GetInterventionsAsync();
        }

        public async Task<IEnumerable<RefRole>> GetAllRolesAsync()
        {
            return await _referentielStore.GetRolesAsync();
        }

        public async Task<IEnumerable<RefStatutFacture>> GetAllStatutFacturesAsync()
        {
            return await _referentielStore.GetStatutFacturesAsync();
        }

        public async Task<IEnumerable<RefStatutIntervention>> GetAllStatutInterventionsAsync()
        {
            return await _referentielStore.GetStatutInterventionsAsync();
        }

        public async Task<IEnumerable<RefTypeContrat>> GetAllTypeContratsAsync()
        {
            return await _referentielStore.GetTypeContratsAsync();
        }

        public async Task<IEnumerable<RefTypeMouvement>> GetAllTypeMouvementsAsync()
        {
            return await _referentielStore.GetTypeMouvementsAsync();
        }

        public async Task<IEnumerable<RefTypePhoto>> GetAllTypePhotosAsync()
        {
            return await _referentielStore.GetTypePhotosAsync();
        }

        public async Task<IEnumerable<RefTypePlan>> GetAllTypePlansAsync()
        {
            return await _referentielStore.GetTypePlansAsync();
        }

        public async Task<IEnumerable<RefTypeeDepenseIntervention>> GetAllTypeDepenseInterventionAsync()
        {
            return await _referentielStore.GetTypeDepenseInterventionsAsync();
        }

        public async Task<IEnumerable<RefTypeVehicule>> GetAllTypeVehiculesAsync()
        {
            return await _referentielStore.GetTypeVehiculesAsync();
        }

        public async Task<IEnumerable<RefStatutVehicule>> GetAllStatusVehiculeAsync()
        {
            return await _referentielStore.GetStatusVehiculeAsync();
        }

        public async Task<IEnumerable<RefTypeDepenseVehicule>> GetAllTypeDepenseVehiculeAsync()
        {
            return await _referentielStore.GetTypeDepenseVehiculeAsync();
        }
    }
}
