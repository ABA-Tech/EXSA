using Domain.Models;

namespace Domain.Stores
{
    public interface IInterventionStore : IRepository<Intervention>
    {
        Task<AffectationIntervention> AddAffectationInterventionAsync(AffectationIntervention affectationIntervention);
        Task<IEnumerable<AffectationIntervention>> GetAffectationsAsync();
        Task RemoveAffectationAsync(AffectationIntervention affectation);
    }
}
