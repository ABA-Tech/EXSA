using Domain.Models;
using Domain.Models.Dto;

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

        Task<bool> CreateDepenseInterventionAsync(SaisieDepenseInterventionDto saisieDepenseIntervention);

        Task<RationTransportGridDto> GetGrilleRationTransportAsync(Guid idIntervention);
        Task<bool> UpdateDepenseIntervention(Guid idIntervention, PathDepenseInterventionDto depenseInterventionDto);
    }
}
