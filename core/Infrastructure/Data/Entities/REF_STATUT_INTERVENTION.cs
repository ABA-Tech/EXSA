using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_STATUT_INTERVENTION
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public byte ORDRE { get; set; }

    public virtual ICollection<INTERVENTION> INTERVENTIONs { get; set; } = new List<INTERVENTION>();
}
