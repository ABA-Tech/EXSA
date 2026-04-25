using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class RefStatutFacture
{
    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();
}
