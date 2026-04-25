using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_STATUT_FACTURE
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public virtual ICollection<FACTURE> FACTUREs { get; set; } = new List<FACTURE>();
}
