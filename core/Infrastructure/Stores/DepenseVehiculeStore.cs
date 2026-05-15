using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Stores;

public class DepenseVehiculeStore : IDepenseVehiculeStore
{
    private readonly ExsaDbContext _context;

    public DepenseVehiculeStore(ExsaDbContext context)
    {
        _context = context;
    }

    public async Task<DepenseVehicule?> GetByIdAsync(Guid id)
    {
        var entity = await _context.DEPENSE_VEHICULEs
            .Include(x => x.ID_VEHICULENavigation)
            .Include(x => x.ID_INTERVENTIONNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID_DEPENSE == id);

        return entity?.ToModel();
    }

    public async Task<IEnumerable<DepenseVehicule>> GetAllAsync()
    {
        var entities = await _context.DEPENSE_VEHICULEs
            .Include(x=>x.ID_VEHICULENavigation)
            .Include(x=>x.ID_INTERVENTIONNavigation)
            .AsNoTracking()
            .ToListAsync();

        return entities.ToModelCollection();
    }

    public async Task<IEnumerable<DepenseVehicule>> GetByVehiculeAsync(Guid idVehicule)
    {
        var entities = await _context.DEPENSE_VEHICULEs
            .AsNoTracking()
            .Where(x => x.ID_VEHICULE == idVehicule)
            .ToListAsync();

        return entities.ToModelCollection();
    }

    public async Task<DepenseVehicule> CreateAsync(DepenseVehicule depense)
    {
        var entity = depense.ToEntity();

        await _context.DEPENSE_VEHICULEs.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.ToModel();
    }

    public async Task<DepenseVehicule> UpdateAsync(DepenseVehicule depense)
    {
        var entity = await _context.DEPENSE_VEHICULEs
            .FirstAsync(x => x.ID_DEPENSE == depense.IdDepense);

        entity.TYPE_DEPENSE = depense.TypeDepense;
        entity.MONTANT_XAF = depense.MontantXaf;
        entity.DATE_DEPENSE = depense.DateDepense;
        entity.DESCRIPTION = depense.Description;
        entity.KILOMETRAGE_AU_MOMENT = depense.KilometrageAuMoment;
        entity.URL_JUSTIFICATIF = depense.UrlJustificatif;
        entity.DATE_MODIFICATION = DateTime.UtcNow;

        _context.Update(entity);
        await _context.SaveChangesAsync();

        return entity.ToModel();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.DEPENSE_VEHICULEs
            .FirstAsync(x => x.ID_DEPENSE == id);

        _context.DEPENSE_VEHICULEs.Remove(entity);

        await _context.SaveChangesAsync();
    }
}