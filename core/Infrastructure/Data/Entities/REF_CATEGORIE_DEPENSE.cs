using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_CATEGORIE_DEPENSE
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public bool? ACTIF { get; set; }
}
