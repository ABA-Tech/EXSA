using Domain.Models;

namespace Domain.Stores
{
    public interface IInterventionStore : IRepository<Intervention>
    {
        Task<AffectationIntervention> AddAffectationInterventionAsync(AffectationIntervention affectationIntervention);
        Task<IEnumerable<AffectationIntervention>> GetAffectationsAsync(Guid IdIntervention);
        Task RemoveAffectationAsync(AffectationIntervention affectation);

        Task<PhotoIntervention?> GetPhotoInterventionByIdAsync(Guid idPhotoIntervention);
        Task<IEnumerable<PhotoIntervention>> GetPhotoInterventionAsync(Guid idIntervention);
        Task DeletePhotoInterventionAsync(PhotoIntervention photoIntervention);
        Task<bool> UploadPhotoInterventionAsync(PhotoIntervention intervention);
    }
}
