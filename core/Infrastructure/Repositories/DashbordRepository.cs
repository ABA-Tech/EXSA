using Domain.Models.Dto;
using Domain.Services;
using Infrastructure.Services;

namespace Infrastructure.Repositories
{
    public class DashbordRepository
    {

        private readonly IDapperService _service;
        public DashbordRepository(IDapperService service)
        {
            _service = service;
        }


        public IEnumerable<KpiDto> GetDashbordKpis(int? annee = null)
        {
            var result = _service.Query<KpiDto>("GET_DASHBORD_GLOBAL_KPIS");

            return result;
        }
         
        public IEnumerable<Alert> GetDashbordAlerts(int? annee = null)
        {
            var result = _service.Query<Alert>("GET_DASHBOARD_GLOBAL_ALERTS");

            return result;
        }
    }
}
