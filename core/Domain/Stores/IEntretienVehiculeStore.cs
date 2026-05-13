using Domain.Models;

namespace Domain.Stores;

public interface IEntretienVehiculeStore
{
    Task<EntretienVehicule?> GetByIdAsync(Guid id);

    Task<IEnumerable<EntretienVehicule>> GetAllAsync();

    Task<IEnumerable<EntretienVehicule>> GetByVehiculeAsync(Guid idVehicule);

    Task<EntretienVehicule> CreateAsync(EntretienVehicule entretien);

    Task<EntretienVehicule> UpdateAsync(EntretienVehicule entretien);

    Task DeleteAsync(Guid id);
}