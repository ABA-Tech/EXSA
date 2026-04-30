using Domain.Models;

namespace Domain.Services
{
    public interface IInterventionService: IAppService<Intervention>
    {
        Task<AffectationIntervention> CreateAffectationInterventionAsync(AffectationIntervention affectationIntervention);
        Task<IEnumerable<AffectationIntervention>> GetAllAffectationsAsync(Guid IdIntervention);
        Task RemoveAffectationAsync(AffectationIntervention affectation);
    }
}
