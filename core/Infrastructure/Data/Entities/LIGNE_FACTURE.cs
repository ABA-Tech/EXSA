using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class LIGNE_FACTURE
{
    public Guid ID_LIGNE { get; set; }

    public Guid ID_FACTURE { get; set; }

    public string DESCRIPTION { get; set; } = null!;

    public decimal QUANTITE { get; set; }

    public decimal PRIX_UNITAIRE { get; set; }

    public decimal TOTAL_XAF { get; set; }

    public virtual FACTURE ID_FACTURENavigation { get; set; } = null!;
}
