using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REGLEMENT
{
    public Guid ID_REGLEMENT { get; set; }

    public Guid ID_FACTURE { get; set; }

    public decimal MONTANT_XAF { get; set; }

    public string MODE_REGLEMENT { get; set; } = null!;

    public string? REFERENCE_REGLEMENT { get; set; }

    public DateTime? DATE_REGLEMENT { get; set; }

    public DateTime? DATE_CREATION { get; set; }

    public DateTime? DATE_MODIFICATION { get; set; }

    public virtual FACTURE ID_FACTURENavigation { get; set; } = null!;
}
