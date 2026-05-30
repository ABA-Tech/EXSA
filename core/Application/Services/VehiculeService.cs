using Domain.Models;
using Domain.Services;
using Domain.Stores;

namespace Application.Services;

public class VehiculeService : IVehiculeService
{
    private readonly IVehiculeStore _vehiculeStore;
    private readonly IInterventionStore _InterventionStore;

    public VehiculeService(IVehiculeStore vehiculeStore, IInterventionStore interventionStore)
    {
        _vehiculeStore = vehiculeStore;
        _InterventionStore = interventionStore;
    }

    public async Task<Vehicule?> GetByIdAsync(Guid id)
    {
        return await _vehiculeStore.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Vehicule>> GetAllAsync()
    {
        var interventions = (await _InterventionStore.GetAllAsync()).First();
        var affect = await _InterventionStore.GetAffectationsAsync(interventions.IdIntervention.Value);
        interventions.AffectationInterventions = affect.ToList();
        var doc = new DocumentService(interventions);
        doc.CreateDocument();

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