using Domain.Models;

namespace Domain.Stores;

public interface IDepenseVehiculeStore
{
    Task<DepenseVehicule?> GetByIdAsync(Guid id);

    Task<IEnumerable<DepenseVehicule>> GetAllAsync();

    Task<IEnumerable<DepenseVehicule>> GetByVehiculeAsync(Guid idVehicule);

    Task<DepenseVehicule> CreateAsync(DepenseVehicule depense);

    Task<DepenseVehicule> UpdateAsync(DepenseVehicule depense);

    Task DeleteAsync(Guid id);
}