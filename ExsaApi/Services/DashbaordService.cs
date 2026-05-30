using Domain.Models.Dto;
using Domain.Services;
using Infrastructure.Repositories;

namespace ExsaApi.Services
{
    public class DashbaordService
    {
        private readonly DashbordRepository _dashbordRepository;

        public DashbaordService(IDapperService dapperService)
        {
            _dashbordRepository = new DashbordRepository(dapperService);
        }

        public DashboardDto GetDashbord()
        {
            DashboardDto dashboardDto= new DashboardDto();
            dashboardDto.Kpis = _dashbordRepository.GetDashbordKpis();
            dashboardDto.Alerts = _dashbordRepository.GetDashbordAlerts();

            return dashboardDto;
        }
    }
}
