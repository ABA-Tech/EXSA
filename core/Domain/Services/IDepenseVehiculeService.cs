using Domain.Models;

namespace Domain.Services;

public interface IDepenseVehiculeService
{
    Task<DepenseVehicule?> GetByIdAsync(Guid id);

    Task<IEnumerable<DepenseVehicule>> GetAllAsync();

    Task<IEnumerable<DepenseVehicule>> GetByVehiculeAsync(Guid idVehicule);

    Task<DepenseVehicule> CreateAsync(DepenseVehicule depense);

    Task<DepenseVehicule> UpdateAsync(DepenseVehicule depense);

    Task DeleteAsync(Guid id);
}