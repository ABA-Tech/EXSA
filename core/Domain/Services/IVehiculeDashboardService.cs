using Domain.Models.Vues;

namespace Domain.Services
{
    public interface IVehiculeDashboardService
    {
        Task<IEnumerable<VehiculeDashboard>> GetAllAsync();

        Task<VehiculeDashboard?> GetByVehiculeAsync(Guid idVehicule);
    }

    public interface IAlerteVehiculeService
    {
        Task<IEnumerable<AlerteVehicule>> GetAllAsync();

        Task<IEnumerable<AlerteVehicule>> GetByVehiculeAsync(Guid idVehicule);
    }

    public interface IDepenseParVehiculeService
    {
        Task<IEnumerable<DepenseParVehicule>> GetAllAsync();

        Task<IEnumerable<DepenseParVehicule>> GetByVehiculeAsync(Guid idVehicule);
    }
}
