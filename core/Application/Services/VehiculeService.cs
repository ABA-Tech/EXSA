using Domain.Models;
using Domain.Services;
using Domain.Stores;

namespace Application.Services;

public class VehiculeService : IVehiculeService
{
    private readonly IVehiculeStore _vehiculeStore;

    public VehiculeService(IVehiculeStore vehiculeStore)
    {
        _vehiculeStore = vehiculeStore;
    }

    public async Task<Vehicule?> GetByIdAsync(Guid id)
    {
        return await _vehiculeStore.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Vehicule>> GetAllAsync()
    {
        return await _vehiculeStore.GetAllAsync();
    }

    public async Task<IEnumerable<Vehicule>> GetByLocataireAsync(Guid idLocataire)
    {
        return await _vehiculeStore.GetByLocataireAsync(idLocataire);
    }

    public async Task<Vehicule> CreateAsync(Vehicule vehicule)
    {
        vehicule.IdVehicule = Guid.NewGuid();
        vehicule.DateCreation = DateTime.UtcNow;

        return await _vehiculeStore.CreateAsync(vehicule);
    }

    public async Task<Vehicule> UpdateAsync(Vehicule vehicule)
    {
        vehicule.DateModification = DateTime.UtcNow;

        return await _vehiculeStore.UpdateAsync(vehicule);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _vehiculeStore.DeleteAsync(id);
    }
}