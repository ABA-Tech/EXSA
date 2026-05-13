using Domain.Models.Vues;
using Domain.Services;
using Domain.Stores;

namespace Application.Services
{
    public class VehiculeDashboardService : IVehiculeDashboardService
    {
        private readonly IVehiculeDashboardStore _store;

        public VehiculeDashboardService(IVehiculeDashboardStore store)
        {
            _store = store;
        }

        public async Task<IEnumerable<VehiculeDashboard>> GetAllAsync()
        {
            return await _store.GetAllAsync();
        }

        public async Task<VehiculeDashboard?> GetByVehiculeAsync(Guid idVehicule)
        {
            return await _store.GetByVehiculeAsync(idVehicule);
        }
    }

    public class DepenseParVehiculeService : IDepenseParVehiculeService
    {
        private readonly IDepenseParVehiculeStore _store;

        public DepenseParVehiculeService(IDepenseParVehiculeStore store)
        {
            _store = store;
        }

        public async Task<IEnumerable<DepenseParVehicule>> GetAllAsync()
        {
            return await _store.GetAllAsync();
        }

        public async Task<IEnumerable<DepenseParVehicule>> GetByVehiculeAsync(Guid idVehicule)
        {
            return await _store.GetByVehiculeAsync(idVehicule);
        }
    }

    public class AlerteVehiculeService : IAlerteVehiculeService
    {
        private readonly IAlerteVehiculeStore _store;

        public AlerteVehiculeService(IAlerteVehiculeStore store)
        {
            _store = store;
        }

        public async Task<IEnumerable<AlerteVehicule>> GetAllAsync()
        {
            return await _store.GetAllAsync();
        }

        public async Task<IEnumerable<AlerteVehicule>> GetByVehiculeAsync(Guid idVehicule)
        {
            return await _store.GetByVehiculeAsync(idVehicule);
        }
    }
}
