using Domain.Models.Vues;

namespace Domain.Stores
{
    public interface IVehiculeDashboardStore
    {
        Task<IEnumerable<VehiculeDashboard>> GetAllAsync();

        Task<VehiculeDashboard?> GetByVehiculeAsync(Guid idVehicule);
    }

    public interface IDepenseParVehiculeStore
    {
        Task<IEnumerable<DepenseParVehicule>> GetAllAsync();

        Task<IEnumerable<DepenseParVehicule>> GetByVehiculeAsync(Guid idVehicule);
    }

    public interface IAlerteVehiculeStore
    {
        Task<IEnumerable<AlerteVehicule>> GetAllAsync();

        Task<IEnumerable<AlerteVehicule>> GetByVehiculeAsync(Guid idVehicule);
    }
}
