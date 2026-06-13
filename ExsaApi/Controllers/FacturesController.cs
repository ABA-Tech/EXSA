using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FacturesController : ControllerBase
    {
        private readonly IFactureService _factureService;

        public FacturesController(IFactureService factureService)
        {
            _factureService = factureService ?? throw new ArgumentNullException(nameof(factureService));
        }

        // GET api/factures
        [HttpGet]
        public async Task<IActionResult> GetAll(string statut = null)
        {
            var factures = await _factureService.GetAllAsync(statut);
            return Ok(factures);
        }

        // GET api/factures/{id}
        [HttpGet("{idFacture}")]
        public async Task<IActionResult> GetById(Guid idFacture)
        {
            var facture = await _factureService.GetWithDetailsAsync(idFacture);
            return Ok(facture);
        }

        // GET api/factures/intervention/{idIntervention}
        [HttpGet("intervention/{idIntervention}")]
        public async Task<IActionResult> GetByIntervention(Guid idIntervention)
        {
            var factures = await _factureService.GetByInterventionAsync(idIntervention);
            return Ok(factures.FirstOrDefault());
        }

        // POST api/factures
        [HttpPost]
        public async Task<IActionResult> Create(Facture facture)
        {
            facture.IdFacture = Guid.NewGuid();
            facture.DateCreation = DateTime.Now;
            facture.Statut = "EMISE";

            var created = await _factureService.CreateAsync(facture);
            return Ok(created);
        }

        // PUT api/factures/{id}
        [HttpPut("{idFacture}")]
        public async Task<IActionResult> Update(Guid idFacture, Facture facture)
        {
            facture.IdFacture = idFacture;
            facture.DateModification = DateTime.Now;

            var updated = await _factureService.UpdateAsync(facture);
            return Ok(updated);
        }

        // DELETE api/factures/{id}
        [HttpDelete("{idFacture}")]
        public async Task<IActionResult> Delete(Guid idFacture)
        {
            await _factureService.DeleteByIdAsync(idFacture);
            return NoContent();
        }

        // POST api/factures/{id}/reglements
        [HttpPost("{idFacture}/reglements")]
        public async Task<IActionResult> AddReglement(Guid idFacture, Reglement reglement)
        {
            var created = await _factureService.AddReglementAsync(idFacture, reglement);
            return Ok(created);
        }

        // GET api/factures/{id}/reglements
        [HttpGet("{idFacture}/reglements")]
        public async Task<IActionResult> GetReglements(Guid idFacture)
        {
            var reglements = await _factureService.GetReglementsAsync(idFacture);
            return Ok(reglements);
        }
    }
}