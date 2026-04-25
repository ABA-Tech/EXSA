using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class VUE_FACTURES_EN_RETARD
{
    public Guid ID_FACTURE { get; set; }

    public Guid ID_LOCATAIRE { get; set; }

    public string NOM_LOCATAIRE { get; set; } = null!;

    public string REFERENCE { get; set; } = null!;

    public string NOM_CLIENT { get; set; } = null!;

    public decimal TOTAL_XAF { get; set; }

    public DateTime? DATE_ECHEANCE { get; set; }

    public int? JOURS_RETARD { get; set; }
}
