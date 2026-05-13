using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class V_ALERTES_VEHICULE
{
    public Guid ID_LOCATAIRE { get; set; }

    public Guid ID_VEHICULE { get; set; }

    public string IMMATRICULATION { get; set; } = null!;

    public string TYPE_ALERTE { get; set; } = null!;

    public string? MESSAGE { get; set; }

    public int? JOURS_RETARD { get; set; }

    public string NIVEAU { get; set; } = null!;
}
