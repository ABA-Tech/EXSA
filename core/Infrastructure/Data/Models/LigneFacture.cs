using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class LigneFacture
{
    public Guid IdLigne { get; set; }

    public Guid IdFacture { get; set; }

    public string Description { get; set; } = null!;

    public decimal Quantite { get; set; }

    public decimal PrixUnitaire { get; set; }

    public decimal TotalXaf { get; set; }

    public virtual Facture IdFactureNavigation { get; set; } = null!;
}
