using Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stores
{
    public interface IDashboardStore
    {
        Task<GlobalDashboardDto> GetGlobalAsync(
            Guid idLocataire,
            CancellationToken cancellationToken = default);
    }
}
