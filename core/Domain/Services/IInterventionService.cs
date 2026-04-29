using Domain.Models;

namespace Domain.Services
{
    public interface IInterventionService: IAppService<Intervention>
    {
        Task<AffectationIntervention> CreateAffectationInterventionAsync(AffectationIntervention affectationIntervention);
        Task<IEnumerable<AffectationIntervention>> GetAllAffectationsAsync();
        Task RemoveAffectationAsync(AffectationIntervention affectation);
    }
}
