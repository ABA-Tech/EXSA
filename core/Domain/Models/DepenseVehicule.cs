using Microsoft.AspNetCore.Http;
using System;

namespace Domain.Models;

public class DepenseVehicule
{
    public Guid IdDepense { get; set; }

    public Guid IdLocataire { get; set; }

    public Guid IdVehicule { get; set; }

    public Guid? IdIntervention { get; set; }

    public Guid IdSaisiePar { get; set; }

    public string TypeDepense { get; set; } = string.Empty;

    public decimal MontantXaf { get; set; }

    public DateTime DateDepense { get; set; }

    public string? Description { get; set; }

    public int? KilometrageAuMoment { get; set; }

    public string? UrlJustificatif { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public IFormFile? FichierJustificatif { get; set; }

    public virtual Vehicule? Vehicule { get; set; }
    public virtual Intervention? Intervention { get; set; }
}