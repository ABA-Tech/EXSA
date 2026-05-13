using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_TYPE_DEPENSE_VEHICULE
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public bool? EST_DEDUCTIBLE { get; set; }

    public string? ICONE { get; set; }

    public virtual ICollection<DEPENSE_VEHICULE> DEPENSE_VEHICULEs { get; set; } = new List<DEPENSE_VEHICULE>();
}
