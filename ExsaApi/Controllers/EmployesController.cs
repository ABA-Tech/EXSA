using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly IGenericService<Employe> _employeService;

        public EmployesController(IGenericService<Employe> genericService)
        {
            _employeService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployes() 
        { 
            return Ok(await _employeService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmploye(Employe employe)
        {
            return Ok(await _employeService.CreateAsync(employe));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployeById(Guid Id)
        {
            return Ok(await _employeService.GetByIdAsync(Id));
        }
    }
}
