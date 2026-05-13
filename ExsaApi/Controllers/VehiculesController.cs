using Application.Services;
using Domain.Models;
using Domain.Models.Vues;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExsaApi.Controllers;

[ApiController]
[Route("api/vehicules")]
public class VehiculesController : ControllerBase
{
    private readonly IVehiculeService _vehiculeService;
    private readonly IDepenseVehiculeService _depenseService;
    private readonly IEntretienVehiculeService _entretienService;
    private readonly IVehiculeDashboardService _dashboardService;
    private readonly IDepenseParVehiculeService _depenseParVehiculeService;
    private readonly IAlerteVehiculeService _alerteVehiculeService;

    public VehiculesController(
        IVehiculeService vehiculeService,
        IDepenseVehiculeService depenseService,
        IEntretienVehiculeService entretienService,
        IVehiculeDashboardService dashboardService,
        IDepenseParVehiculeService depenseParVehiculeService,
        IAlerteVehiculeService alerteVehiculeService)
    {
        _vehiculeService = vehiculeService;
        _depenseService = depenseService;
        _entretienService = entretienService;
        _dashboardService = dashboardService;
        _depenseParVehiculeService = depenseParVehiculeService;
        _alerteVehiculeService = alerteVehiculeService;
    }

    #region VEHICULES

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicule>>> GetAllVehicules()
    {
        var vehicules = await _vehiculeService.GetAllAsync();

        return Ok(vehicules);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Vehicule>> GetVehiculeById(Guid id)
    {
        var vehicule = await _vehiculeService.GetByIdAsync(id);

        if (vehicule == null)
        {
            return NotFound();
        }

        return Ok(vehicule);
    }

    [HttpGet("locataire/{idLocataire:guid}")]
    public async Task<ActionResult<IEnumerable<Vehicule>>> GetVehiculesByLocataire(Guid idLocataire)
    {
        var vehicules = await _vehiculeService.GetByLocataireAsync(idLocataire);

        return Ok(vehicules);
    }

    [HttpPost]
    public async Task<ActionResult<Vehicule>> CreateVehicule([FromBody] Vehicule vehicule)
    {
        var created = await _vehiculeService.CreateAsync(vehicule);

        return CreatedAtAction(
            nameof(GetVehiculeById),
            new { id = created.IdVehicule },
            created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Vehicule>> UpdateVehicule(Guid id, [FromBody] Vehicule vehicule)
    {
        if (id != vehicule.IdVehicule)
        {
            return BadRequest("L'identifiant ne correspond pas.");
        }

        var updated = await _vehiculeService.UpdateAsync(vehicule);

        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteVehicule(Guid id)
    {
        await _vehiculeService.DeleteAsync(id);

        return NoContent();
    }

    #endregion

    #region DEPENSES

    [HttpGet("{idVehicule:guid}/depenses")]
    public async Task<ActionResult<IEnumerable<DepenseVehicule>>> GetDepensesByVehicule(Guid idVehicule)
    {
        var depenses = await _depenseService.GetByVehiculeAsync(idVehicule);

        return Ok(depenses);
    }

    [HttpGet("depenses/{id:guid}")]
    public async Task<ActionResult<DepenseVehicule>> GetDepenseById(Guid id)
    {
        var depense = await _depenseService.GetByIdAsync(id);

        if (depense == null)
        {
            return NotFound();
        }

        return Ok(depense);
    }

    [HttpPost("{idVehicule:guid}/depenses")]
    public async Task<ActionResult<DepenseVehicule>> CreateDepense(
        Guid idVehicule,
        [FromBody] DepenseVehicule depense)
    {
        depense.IdVehicule = idVehicule;

        var created = await _depenseService.CreateAsync(depense);

        return CreatedAtAction(
            nameof(GetDepenseById),
            new { id = created.IdDepense },
            created);
    }

    [HttpPut("depenses/{id:guid}")]
    public async Task<ActionResult<DepenseVehicule>> UpdateDepense(
        Guid id,
        [FromBody] DepenseVehicule depense)
    {
        if (id != depense.IdDepense)
        {
            return BadRequest("L'identifiant ne correspond pas.");
        }

        var updated = await _depenseService.UpdateAsync(depense);

        return Ok(updated);
    }

    [HttpDelete("depenses/{id:guid}")]
    public async Task<IActionResult> DeleteDepense(Guid id)
    {
        await _depenseService.DeleteAsync(id);

        return NoContent();
    }

    #endregion

    #region ENTRETIENS

    [HttpGet("{idVehicule:guid}/entretiens")]
    public async Task<ActionResult<IEnumerable<EntretienVehicule>>> GetEntretiensByVehicule(Guid idVehicule)
    {
        var entretiens = await _entretienService.GetByVehiculeAsync(idVehicule);

        return Ok(entretiens);
    }

    [HttpGet("entretiens/{id:guid}")]
    public async Task<ActionResult<EntretienVehicule>> GetEntretienById(Guid id)
    {
        var entretien = await _entretienService.GetByIdAsync(id);

        if (entretien == null)
        {
            return NotFound();
        }

        return Ok(entretien);
    }

    [HttpPost("{idVehicule:guid}/entretiens")]
    public async Task<ActionResult<EntretienVehicule>> CreateEntretien(
        Guid idVehicule,
        [FromBody] EntretienVehicule entretien)
    {
        entretien.IdVehicule = idVehicule;

        var created = await _entretienService.CreateAsync(entretien);

        return CreatedAtAction(
            nameof(GetEntretienById),
            new { id = created.IdEntretien },
            created);
    }

    [HttpPut("entretiens/{id:guid}")]
    public async Task<ActionResult<EntretienVehicule>> UpdateEntretien(
        Guid id,
        [FromBody] EntretienVehicule entretien)
    {
        if (id != entretien.IdEntretien)
        {
            return BadRequest("L'identifiant ne correspond pas.");
        }

        var updated = await _entretienService.UpdateAsync(entretien);

        return Ok(updated);
    }

    [HttpDelete("entretiens/{id:guid}")]
    public async Task<IActionResult> DeleteEntretien(Guid id)
    {
        await _entretienService.DeleteAsync(id);

        return NoContent();
    }

    #endregion

    [HttpGet("dashboard")]
    public async Task<ActionResult<IEnumerable<VehiculeDashboard>>> GetDashboard()
    {
        var result = await _dashboardService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{idVehicule:guid}/dashboard")]
    public async Task<ActionResult<VehiculeDashboard>> GetVehiculeDashboard(Guid idVehicule)
    {
        var result = await _dashboardService.GetByVehiculeAsync(idVehicule);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("dashboard/depenses")]
    public async Task<ActionResult<IEnumerable<DepenseParVehicule>>> GetDepensesDashboard()
    {
        var result = await _depenseParVehiculeService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{idVehicule:guid}/dashboard/depenses")]
    public async Task<ActionResult<IEnumerable<DepenseParVehicule>>> GetDepensesDashboardByVehicule(Guid idVehicule)
    {
        var result = await _depenseParVehiculeService.GetByVehiculeAsync(idVehicule);

        return Ok(result);
    }

    [HttpGet("dashboard/alertes")]
    public async Task<ActionResult<IEnumerable<AlerteVehicule>>> GetAlertes()
    {
        var result = await _alerteVehiculeService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{idVehicule:guid}/dashboard/alertes")]
    public async Task<ActionResult<IEnumerable<AlerteVehicule>>> GetAlertesByVehicule(Guid idVehicule)
    {
        var result = await _alerteVehiculeService.GetByVehiculeAsync(idVehicule);

        return Ok(result);
    }
}