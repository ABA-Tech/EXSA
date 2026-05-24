using Domain.Models.Dto;
using Domain.Services;
using Domain.Stores;

namespace Application.Services;

public sealed class DashboardService : IDashboardService
{
    private readonly IDashboardStore _dashboardStore;

    public DashboardService(IDashboardStore dashboardStore)
    {
        _dashboardStore = dashboardStore
            ?? throw new ApplicationException(nameof(dashboardStore));
    }

    public Task<GlobalDashboardDto> GetGlobalAsync(
        Guid idLocataire,
        CancellationToken cancellationToken = default)
    {
        return _dashboardStore.GetGlobalAsync(idLocataire, cancellationToken);
    }
}