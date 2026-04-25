using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class VueFacturesEnRetard
{
    public Guid IdFacture { get; set; }

    public Guid IdLocataire { get; set; }

    public string NomLocataire { get; set; } = null!;

    public string Reference { get; set; } = null!;

    public string NomClient { get; set; } = null!;

    public decimal TotalXaf { get; set; }

    public DateTime? DateEcheance { get; set; }

    public int? JoursRetard { get; set; }
}
