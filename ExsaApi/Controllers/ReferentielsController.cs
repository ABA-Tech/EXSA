using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferentielsController : ControllerBase
    {
        private readonly IReferentielService _referentielService;

        public ReferentielsController(IReferentielService referentielService)
        {
            _referentielService = referentielService;
        }

        [HttpGet("GetStatutFactures")]
        public async Task<IActionResult> GetAllStatutFactures()
        {
            return Ok(await _referentielService.GetAllStatutFacturesAsync());
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _referentielService.GetAllRolesAsync());
        }

        [HttpGet("GetTypeContrats")]
        public async Task<IActionResult> GetAllTypeContrats()
        {
            return Ok(await _referentielService.GetAllTypeContratsAsync());
        }

        [HttpGet("GetTypePlans")]
        public async Task<IActionResult> GetAllTypePlans()
        {
            return Ok(await _referentielService.GetAllTypePlansAsync());
        }

        [HttpGet("GetTypePhotos")]
        public async Task<IActionResult> GetAllTypePhotos()
        {
            return Ok(await _referentielService.GetAllTypePhotosAsync());
        }

        [HttpGet("GetTypeInterventions")]
        public async Task<IActionResult> GetAllTypeInterventions()
        {
            return Ok(await _referentielService.GetAllTypeInterventionsAsync());
        }

        [HttpGet("GetTypeMouvements")]
        public async Task<IActionResult> GetAllTypeMouvements()
        {
            return Ok(await _referentielService.GetAllTypeMouvementsAsync());
        }

        [HttpGet("GetStatutInterventions")]
        public async Task<IActionResult> GetAllStatutInterventions()
        {
            return Ok(await _referentielService.GetAllStatutInterventionsAsync());
        }

        [HttpGet("GetTypesDepenseInterventions")]
        public async Task<IActionResult> GetTypesDepenseInterventions()
        {
            return Ok(await _referentielService.GetAllTypeDepenseInterventionAsync());
        }
    }
}
