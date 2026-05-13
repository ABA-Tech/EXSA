using Domain.Models;

namespace Domain.Services;

public interface IVehiculeService
{
    Task<Vehicule?> GetByIdAsync(Guid id);

    Task<IEnumerable<Vehicule>> GetAllAsync();

    Task<IEnumerable<Vehicule>> GetByLocataireAsync(Guid idLocataire);

    Task<Vehicule> CreateAsync(Vehicule vehicule);

    Task<Vehicule> UpdateAsync(Vehicule vehicule);

    Task DeleteAsync(Guid id);
}