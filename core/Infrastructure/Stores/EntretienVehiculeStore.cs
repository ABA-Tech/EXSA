using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Stores;

public class EntretienVehiculeStore : IEntretienVehiculeStore
{
    private readonly ExsaDbContext _context;

    public EntretienVehiculeStore(ExsaDbContext context)
    {
        _context = context;
    }

    public async Task<EntretienVehicule?> GetByIdAsync(Guid id)
    {
        var entity = await _context.ENTRETIEN_VEHICULEs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID_ENTRETIEN == id);

        return entity?.ToModel();
    }

    public async Task<IEnumerable<EntretienVehicule>> GetAllAsync()
    {
        var entities = await _context.ENTRETIEN_VEHICULEs
            .AsNoTracking()
            .ToListAsync();

        return entities.ToModelCollection();
    }

    public async Task<IEnumerable<EntretienVehicule>> GetByVehiculeAsync(Guid idVehicule)
    {
        var entities = await _context.ENTRETIEN_VEHICULEs
            .AsNoTracking()
            .Where(x => x.ID_VEHICULE == idVehicule)
            .ToListAsync();

        return entities.ToModelCollection();
    }

    public async Task<EntretienVehicule> CreateAsync(EntretienVehicule entretien)
    {
        var entity = entretien.ToEntity();

        await _context.ENTRETIEN_VEHICULEs.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.ToModel();
    }

    public async Task<EntretienVehicule> UpdateAsync(EntretienVehicule entretien)
    {
        var entity = await _context.ENTRETIEN_VEHICULEs
            .FirstAsync(x => x.ID_ENTRETIEN == entretien.IdEntretien);

        entity.TYPE_ENTRETIEN = entretien.TypeEntretien;
        entity.DATE_PREVUE = entretien.DatePrevue;
        entity.KILOMETRAGE_PREVU = entretien.KilometragePrevu;
        entity.PRESTATAIRE = entretien.Prestataire;
        entity.DATE_REALISE = entretien.DateRealise;
        entity.KILOMETRAGE_REALISE = entretien.KilometrageRealise;
        entity.COUT_XAF = entretien.CoutXaf;
        entity.STATUT = entretien.Statut;
        entity.NOTES = entretien.Notes;
        entity.DATE_MODIFICATION = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return entity.ToModel();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.ENTRETIEN_VEHICULEs
            .FirstAsync(x => x.ID_ENTRETIEN == id);

        _context.ENTRETIEN_VEHICULEs.Remove(entity);

        await _context.SaveChangesAsync();
    }
}