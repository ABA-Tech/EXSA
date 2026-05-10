using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.Outputs;

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

        Task<int> AddDepenseIntervention(DepenseIntervention saisieDepenseIntervention, bool isUpdate = false);

        Task<IEnumerable<RationTransportOutput>> GetSaisiesRationTransportAsync(Guid idIntervention);
        Task<DepenseIntervention?> GetDepenseRationTransportById(Guid idDepenseIntervention);
        Task<bool> UpdateDepenseRationTransport(DepenseIntervention depenseIntervention);
    }
}
