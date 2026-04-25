using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatairesController : ControllerBase
    {
        private readonly IGenericService<Locataire> _locataireService;

        public LocatairesController(IGenericService<Locataire> locataireService)
        {
            _locataireService = locataireService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocataires()
        {
            return Ok(await _locataireService.GetAllAsync());
        }

    }
}
