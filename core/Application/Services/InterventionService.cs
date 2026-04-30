using Domain.Models;
using Domain.Services;
using Domain.Stores;

namespace Application.Services
{
    public class InterventionService : AppService<Intervention>, IInterventionService
    {
        private readonly IInterventionStore _interventionStore;
        public InterventionService(IInterventionStore interventionStore): base(interventionStore)
        {
            _interventionStore = interventionStore;
        }

        public async Task<AffectationIntervention> CreateAffectationInterventionAsync(AffectationIntervention affectationIntervention)
        {
            return await _interventionStore.AddAffectationInterventionAsync(affectationIntervention);
        }

        public async Task<IEnumerable<AffectationIntervention>> GetAllAffectationsAsync(Guid IdIntervention)
        {
            return await _interventionStore.GetAffectationsAsync(IdIntervention);
        }

        public async Task RemoveAffectationAsync(AffectationIntervention affectation)
        {
            await _interventionStore.RemoveAffectationAsync(affectation);
        }
    }
}
