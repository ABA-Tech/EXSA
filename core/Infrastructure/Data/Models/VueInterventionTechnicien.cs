using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class VueInterventionTechnicien
{
    public Guid IdIntervention { get; set; }

    public Guid IdLocataire { get; set; }

    public string Reference { get; set; } = null!;

    public string Titre { get; set; } = null!;

    public string Type { get; set; } = null!;

    public byte Priorite { get; set; }

    public string Statut { get; set; } = null!;

    public string? NomClient { get; set; }

    public DateTime? DatePlanifiee { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public DateTime? DateValidation { get; set; }

    public string? NomTechnicienPrincipal { get; set; }

    public string? TelTechnicienPrincipal { get; set; }

    public string? NomCreateur { get; set; }
}
