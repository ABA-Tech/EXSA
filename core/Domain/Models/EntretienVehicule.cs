using System;

namespace Domain.Models;

public class EntretienVehicule
{
    public Guid IdEntretien { get; set; }

    public Guid IdLocataire { get; set; }

    public Guid IdVehicule { get; set; }

    public Guid? IdDepense { get; set; }

    public string TypeEntretien { get; set; } = string.Empty;

    public DateTime DatePrevue { get; set; }

    public int? KilometragePrevu { get; set; }

    public string? Prestataire { get; set; }

    public DateTime? DateRealise { get; set; }

    public int? KilometrageRealise { get; set; }

    public decimal? CoutXaf { get; set; }

    public string Statut { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public Guid IdCreateur { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }
}