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

        public async Task<IEnumerable<PhotoIntervention>> GetAllPhotoInterventionAsync(Guid idIntervention)
        {
            return await _interventionStore.GetPhotoInterventionAsync(idIntervention);
        }

        public async Task RemoveAffectationAsync(AffectationIntervention affectation)
        {
            await _interventionStore.RemoveAffectationAsync(affectation);
        }

        public async Task RemovePhotoInterventionAsync(Guid idPhotoIntervention)
        {
            var photoIntervention = await _interventionStore.GetPhotoInterventionByIdAsync(idPhotoIntervention);
            if (photoIntervention == null)
            {
                throw new ApplicationException("Photo inexistante");
            }


            var fileName = Path.GetFileName(photoIntervention.UrlBlob);
            var folder = photoIntervention.IdIntervention.ToString();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", folder, fileName);

            // Supprimer fichier physique
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }


            await _interventionStore.DeletePhotoInterventionAsync(photoIntervention);
        }

        public async Task<bool> UploadPhotoInterventionAsync(PhotoIntervention intervention)
        {
            return await _interventionStore.UploadPhotoInterventionAsync(intervention);
        }
    }
}
