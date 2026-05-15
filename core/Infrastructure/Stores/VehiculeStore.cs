using Domain.Models;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Stores;

public class VehiculeStore : IVehiculeStore
{
    private readonly ExsaDbContext _context;

    public VehiculeStore(ExsaDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicule?> GetByIdAsync(Guid id)
    {
        var entity = await _context.VEHICULEs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID_VEHICULE == id);

        return entity?.ToModel();
    }

    public async Task<IEnumerable<Vehicule>> GetAllAsync()
    {
        var entities = await _context.VEHICULEs
            .AsNoTracking()
            .ToListAsync();

        return entities.ToModelCollection();
    }

    public async Task<IEnumerable<Vehicule>> GetByLocataireAsync(Guid idLocataire)
    {
        var entities = await _context.VEHICULEs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire)
            .ToListAsync();

        return entities.ToModelCollection();
    }

    public async Task<Vehicule> CreateAsync(Vehicule vehicule)
    {
        var entity = vehicule.ToEntity();

        await _context.VEHICULEs.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.ToModel();
    }

    public async Task<Vehicule> UpdateAsync(Vehicule vehicule)
    {
        var entity = await _context.VEHICULEs
            .FirstAsync(x => x.ID_VEHICULE == vehicule.IdVehicule);

        entity.ID_LOCATAIRE = vehicule.IdLocataire;
        entity.IMMATRICULATION = vehicule.Immatriculation;
        entity.MARQUE = vehicule.Marque;
        entity.MODELE = vehicule.Modele;
        entity.ANNEE = vehicule.Annee;
        entity.TYPE_VEHICULE = vehicule.TypeVehicule;
        entity.COULEUR = vehicule.Couleur;
        entity.KILOMETRAGE_ACTUEL = vehicule.KilometrageActuel;
        entity.DATE_ACQUISITION = vehicule.DateAcquisition;
        entity.PRIX_ACQUISITION_XAF = vehicule.PrixAcquisitionXaf;
        entity.ASSURANCE_COMPAGNIE = vehicule.AssuranceCompagnie;
        entity.ASSURANCE_NUMERO = vehicule.AssuranceNumero;
        entity.ASSURANCE_EXPIRATION = vehicule.AssuranceExpiration;
        entity.VIGNETTE_EXPIRATION = vehicule.VignetteExpiration;
        entity.VISITE_TECHNIQUE_EXPIRATION = vehicule.VisiteTechniqueExpiration;
        entity.STATUT = vehicule.Statut;
        entity.URL_PHOTO = vehicule.UrlPhoto;
        entity.NOTES = vehicule.Notes;
        entity.DATE_MODIFICATION = DateTime.UtcNow;
        entity.EST_SUPPRIME = vehicule.EstSupprime;

        _context.VEHICULEs.Update(entity);

        await _context.SaveChangesAsync();

        return entity.ToModel();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.VEHICULEs
            .FirstAsync(x => x.ID_VEHICULE == id);

        _context.VEHICULEs.Remove(entity);

        await _context.SaveChangesAsync();
    }
}