using System.Security.Claims;
using Domain.Models.Dto;
using Domain.Services;
using ExsaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExsaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;
    private readonly DashbaordService _localService;

    public DashboardController(IDashboardService dashboardService, DashbaordService service)
    {
        _dashboardService = dashboardService
            ?? throw new ApplicationException(nameof(dashboardService));
        _localService = service;
    }

    [HttpGet("temp")]
    public ActionResult GetDashbordTmp()
    {
        var result = _localService.GetDashbord();
        return Ok(result);
    }

    [HttpGet("global")]
    public async Task<ActionResult<GlobalDashboardDto>> GetGlobal(
        CancellationToken cancellationToken)
    {
        var idLocataire = GetRequiredGuidClaim("id_locataire");

        var result = await _dashboardService.GetGlobalAsync(
            idLocataire,
            cancellationToken);

        return Ok(result);
    }

    private Guid GetRequiredGuidClaim(string claimType)
    {
        var value = User.FindFirstValue(claimType);

        if (!Guid.TryParse(value, out var id))
        {
            throw new UnauthorizedAccessException(
                $"Claim obligatoire introuvable ou invalide : {claimType}");
        }

        return id;
    }
}