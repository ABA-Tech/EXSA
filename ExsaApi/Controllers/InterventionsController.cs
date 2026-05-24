using Application.Services;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly IInterventionService _interventionService;
        private readonly IGenericService<Locataire> _locataireService;
        private readonly IAppService<Utilisateur> _utilisateurService;

        public InterventionsController(
            IInterventionService interventionService, 
            IGenericService<Locataire> locataireService,
            IAppService<Utilisateur> utilisateurService) 
        {
            _interventionService = interventionService ?? throw new ApplicationException(nameof(interventionService));
            _locataireService = locataireService ?? throw new ApplicationException(nameof(locataireService));
            _utilisateurService = utilisateurService ?? throw new ApplicationException(nameof(utilisateurService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInterventions(string status = null)
        {
            var interventions = await _interventionService.GetAllAsync(status);
            return Ok(interventions);
        }

        // GET api/<InterventionController>/5
        [HttpGet("{idIntervention}")]
        public async Task<IActionResult> GetIntervention(Guid idIntervention)
        {
            var intervention = await _interventionService.GetByIdAsync(idIntervention);
            return Ok(intervention);
        }

        // POST api/<InterventionController>
        [HttpPost]
        public async Task<IActionResult> Post(Intervention intervention)
        {
            var locataire = (await _locataireService.GetAllAsync()).FirstOrDefault();
            if (locataire == null)
            {
                throw new Exception("Locataire non définit");
            }

            var firstUtilisateur = (await _utilisateurService.GetAllAsync()).First();

            intervention.IdCreateur = firstUtilisateur.IdUtilisateur;
            intervention.IdLocataire = locataire.IdLocataire;
            intervention.DateCreation = DateTime.Now;
            intervention.EstSupprime = false;
            intervention.DateModification = DateTime.Now;

            return Ok(await _interventionService.CreateAsync(intervention));
        }

        [HttpPost("CreateAffectation")]
        public async Task<IActionResult> CreateAffectation(IEnumerable<AffectationIntervention> affectationInterventions)
        {
            foreach(var affectation in affectationInterventions)
            {
                await _interventionService.CreateAffectationInterventionAsync(affectation);
            }

            return Ok(affectationInterventions);
        }

        // DELETE api/<InterventionController>/5
        [HttpPost("RemoveAffectation")]
        public async Task<IActionResult> DeleteAffectation(AffectationIntervention affectation)
        {
            await _interventionService.RemoveAffectationAsync(affectation);
            return Ok();
        }

        // DELETE api/<InterventionController>/5
        [HttpPost("RemoveAllAffectation")]
        public async Task<IActionResult> DeleteAllAffectation(IEnumerable<AffectationIntervention> affectations)
        {
            foreach(var affectation in affectations)
                await _interventionService.RemoveAffectationAsync(affectation);
            return Ok();
        }

        // DELETE api/<InterventionController>/5
        [HttpGet("GetAffectations/{IdIntervention}")]
        public async Task<IActionResult> GetAffectations(Guid IdIntervention)
        {
            var affectations = await _interventionService.GetAllAffectationsAsync(IdIntervention);
            return Ok(affectations);
        }

        // PUT api/<InterventionController>/5
        [HttpPut("{idIntervention}")]
        public async Task<IActionResult> Put(Guid idIntervention, Intervention intervention)
        {
            if(intervention.IdIntervention == null)
            {
                intervention.IdIntervention = idIntervention;
            }

            intervention.DateModification = DateTime.Now;
            return Ok(await _interventionService.UpdateAsync(intervention));
        }

        // PUT api/<InterventionController>/5
        [HttpPut("UpdateStatutIntervention/{idIntervention}")]
        public async Task<IActionResult> UpdateStatut(Guid idIntervention, string statutIntervention)
        {
            var intervention = await _interventionService.GetByIdAsync(idIntervention);

            intervention.Statut = statutIntervention;
            intervention.DateModification = DateTime.Now;
            return Ok(await _interventionService.UpdateAsync(intervention));
        }

        // DELETE api/<InterventionController>/5
        [HttpDelete("{idIntervention}")]
        public async Task<IActionResult> Delete(Guid idIntervention)
        {
            await _interventionService.DeleteByIdAsync(idIntervention);
            return Ok();
        }



        [HttpPost("uploadPhotos")]
        public async Task<IActionResult> UploadPhoto([FromForm] UploadPhotoDto dto)
        {
            if (dto.Files.Length <=0)
                return BadRequest("Fichier invalide");

            // Vérification extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            foreach (var file in dto.Files)
            {
                if(file == null || file.Length == 0)
                {
                    return BadRequest("Fichier invalide");
                }

                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    return BadRequest("Format non autorisé");
            }

            // Cette logique sera reporté dans le service après la mise en place de l'authentification.

            var firstUtilisateur = (await _utilisateurService.GetAllAsync()).First();

            // Dossier par intervention
            var folderPath = Path.Combine("wwwroot/uploads", dto.IdIntervention.ToString());

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            foreach (var file in dto.Files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                var fileName = $"{Guid.NewGuid()}{extension}";
                var fullPath = Path.Combine(folderPath, fileName);

                // Sauvegarde physique
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var url = $"{Request.Scheme}://{Request.Host}/uploads/{dto.IdIntervention}/{fileName}";

                var photo = new PhotoIntervention
                {
                    IdPhoto = null,
                    IdIntervention = dto.IdIntervention,
                    UrlBlob = url,
                    TypePhoto = dto.TypePhoto,
                    DatePrise = dto.DatePrise,
                    IdUploadeur = firstUtilisateur.IdUtilisateur // à remplacer par ton user
                };
            
                var res = await _interventionService.UploadPhotoInterventionAsync(photo);
            }


            return Ok(true);
        }


        [HttpDelete("RemovePhoto/{idPhotoIntervention}")]
        public async Task<IActionResult> DeletePhoto(Guid idPhotoIntervention)
        {
            await _interventionService.RemovePhotoInterventionAsync(idPhotoIntervention);

            return NoContent();
        }

        [HttpGet("GetPhotosIntervention/{idIntervention}")]
        public async Task<IActionResult> GetPhotos(Guid idIntervention) 
        { 
            return Ok(await _interventionService.GetAllPhotoInterventionAsync(idIntervention));
        }

        [HttpPost("SaisieDepenseIntervention")]
        public async Task<IActionResult> GetPhotos(SaisieDepenseInterventionDto depense) 
        { 
            return Ok(await _interventionService.CreateDepenseInterventionAsync(depense));
        }

        [HttpPost("ReturnSuccess")]
        public ActionResult ReturnTrue([FromForm] List<byte[]> demo) 
        { 
            return Ok( new { success =  true });
        }


        [HttpGet("GetGrilleRationTransport/{idIntervention}")]
        public async Task<IActionResult> GetGrilleRationTransport(Guid idIntervention)
        {
            return Ok(await _interventionService.GetGrilleRationTransportAsync(idIntervention));
        }


        [HttpPatch("PatchDepenseIntervention/{idIntervention}")]
        public async Task<IActionResult> PatchDepenseIntervention(Guid idIntervention, PathDepenseInterventionDto pathDepense)
        {
            return Ok(await _interventionService.UpdateDepenseIntervention(idIntervention, pathDepense));
        }
    }
}
