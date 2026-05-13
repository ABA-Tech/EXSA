using Domain.Models;
using Domain.Services;
using Domain.Stores;

namespace Application.Services;

public class DepenseVehiculeService : IDepenseVehiculeService
{
    private readonly IDepenseVehiculeStore _depenseStore;

    public DepenseVehiculeService(IDepenseVehiculeStore depenseStore)
    {
        _depenseStore = depenseStore;
    }

    public async Task<DepenseVehicule?> GetByIdAsync(Guid id)
    {
        return await _depenseStore.GetByIdAsync(id);
    }

    public async Task<IEnumerable<DepenseVehicule>> GetAllAsync()
    {
        return await _depenseStore.GetAllAsync();
    }

    public async Task<IEnumerable<DepenseVehicule>> GetByVehiculeAsync(Guid idVehicule)
    {
        return await _depenseStore.GetByVehiculeAsync(idVehicule);
    }

    public async Task<DepenseVehicule> CreateAsync(DepenseVehicule depense)
    {
        depense.IdDepense = Guid.NewGuid();
        depense.DateCreation = DateTime.UtcNow;

        return await _depenseStore.CreateAsync(depense);
    }

    public async Task<DepenseVehicule> UpdateAsync(DepenseVehicule depense)
    {
        depense.DateModification = DateTime.UtcNow;

        return await _depenseStore.UpdateAsync(depense);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _depenseStore.DeleteAsync(id);
    }
}