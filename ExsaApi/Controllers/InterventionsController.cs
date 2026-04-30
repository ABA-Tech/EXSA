using Application.Services;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAllInterventions()
        {
            var interventions = await _interventionService.GetAllAsync();
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
    }
}
