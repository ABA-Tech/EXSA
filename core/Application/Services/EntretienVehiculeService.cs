using Domain.Models;
using Domain.Services;
using Domain.Stores;

namespace Application.Services;

public class EntretienVehiculeService : IEntretienVehiculeService
{
    private readonly IEntretienVehiculeStore _entretienStore;

    public EntretienVehiculeService(IEntretienVehiculeStore entretienStore)
    {
        _entretienStore = entretienStore;
    }

    public async Task<EntretienVehicule?> GetByIdAsync(Guid id)
    {
        return await _entretienStore.GetByIdAsync(id);
    }

    public async Task<IEnumerable<EntretienVehicule>> GetAllAsync()
    {
        return await _entretienStore.GetAllAsync();
    }

    public async Task<IEnumerable<EntretienVehicule>> GetByVehiculeAsync(Guid idVehicule)
    {
        return await _entretienStore.GetByVehiculeAsync(idVehicule);
    }

    public async Task<EntretienVehicule> CreateAsync(EntretienVehicule entretien)
    {
        entretien.IdEntretien = Guid.NewGuid();
        entretien.DateCreation = DateTime.UtcNow;

        return await _entretienStore.CreateAsync(entretien);
    }

    public async Task<EntretienVehicule> UpdateAsync(EntretienVehicule entretien)
    {
        entretien.DateModification = DateTime.UtcNow;

        return await _entretienStore.UpdateAsync(entretien);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _entretienStore.DeleteAsync(id);
    }
}