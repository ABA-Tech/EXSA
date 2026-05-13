using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_TYPE_VEHICULE
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public virtual ICollection<VEHICULE> VEHICULEs { get; set; } = new List<VEHICULE>();
}
