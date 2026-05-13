using Domain.Models.Vues;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Stores
{
    public class DepenseParVehiculeStore : IDepenseParVehiculeStore
    {
        private readonly ExsaDbContext _context;

        public DepenseParVehiculeStore(ExsaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepenseParVehicule>> GetAllAsync()
        {
            var entities = await _context.V_DEPENSES_PAR_VEHICULEs
                .AsNoTracking()
                .ToListAsync();

            return entities.ToModelCollection();
        }

        public async Task<IEnumerable<DepenseParVehicule>> GetByVehiculeAsync(Guid idVehicule)
        {
            var entities = await _context.V_DEPENSES_PAR_VEHICULEs
                .AsNoTracking()
                .Where(x => x.ID_INTERVENTION == idVehicule)
                .ToListAsync();

            return entities.ToModelCollection();
        }
    }

    public class AlerteVehiculeStore : IAlerteVehiculeStore
    {
        private readonly ExsaDbContext _context;

        public AlerteVehiculeStore(ExsaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlerteVehicule>> GetAllAsync()
        {
            var entities = await _context.V_ALERTES_VEHICULEs
                .AsNoTracking()
                .ToListAsync();

            return entities.ToModelCollection();
        }

        public async Task<IEnumerable<AlerteVehicule>> GetByVehiculeAsync(Guid idVehicule)
        {
            var entities = await _context.V_ALERTES_VEHICULEs
                .AsNoTracking()
                .Where(x => x.ID_VEHICULE == idVehicule)
                .ToListAsync();

            return entities.ToModelCollection();
        }
    }

    public class VehiculeDashboardStore : IVehiculeDashboardStore
    {
        private readonly ExsaDbContext _context;

        public VehiculeDashboardStore(ExsaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehiculeDashboard>> GetAllAsync()
        {
            var entities = await _context.V_VEHICULE_DASHBOARDs
                .AsNoTracking()
                .ToListAsync();

            return entities.ToModelCollection();
        }

        public async Task<VehiculeDashboard?> GetByVehiculeAsync(Guid idVehicule)
        {
            var entity = await _context.V_VEHICULE_DASHBOARDs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID_VEHICULE == idVehicule);

            return entity?.ToModel();
        }
    }
}
