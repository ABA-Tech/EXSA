using Domain.Models;

namespace Domain.Services
{
    public interface IInterventionService: IAppService<Intervention>
    {
        Task<AffectationIntervention> CreateAffectationInterventionAsync(AffectationIntervention affectationIntervention);
        Task<IEnumerable<AffectationIntervention>> GetAllAffectationsAsync(Guid IdIntervention);
        Task RemoveAffectationAsync(AffectationIntervention affectation);

        Task<IEnumerable<PhotoIntervention>> GetAllPhotoInterventionAsync(Guid idIntervention);
        Task RemovePhotoInterventionAsync(Guid idPhotoIntervention);
        Task<bool> UploadPhotoInterventionAsync(PhotoIntervention intervention);
    }
}
