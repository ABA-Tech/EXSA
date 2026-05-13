using System;

namespace Domain.Models;

public class Vehicule
{
    public Guid IdVehicule { get; set; }

    public Guid IdLocataire { get; set; }

    public string Immatriculation { get; set; } = string.Empty;

    public string Marque { get; set; } = string.Empty;

    public string Modele { get; set; } = string.Empty;

    public short Annee { get; set; }

    public string TypeVehicule { get; set; } = string.Empty;

    public string? Couleur { get; set; }

    public int KilometrageActuel { get; set; }

    public DateTime? DateAcquisition { get; set; }

    public decimal? PrixAcquisitionXaf { get; set; }

    public string? AssuranceCompagnie { get; set; }

    public string? AssuranceNumero { get; set; }

    public DateTime? AssuranceExpiration { get; set; }

    public DateTime? VignetteExpiration { get; set; }

    public DateTime? VisiteTechniqueExpiration { get; set; }

    public string Statut { get; set; } = string.Empty;

    public string? UrlPhoto { get; set; }

    public string? Notes { get; set; }

    public Guid IdCreateur { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public bool EstSupprime { get; set; }
}