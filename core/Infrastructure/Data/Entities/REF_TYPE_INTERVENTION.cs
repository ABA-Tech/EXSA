using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_TYPE_INTERVENTION
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public virtual ICollection<INTERVENTION> INTERVENTIONs { get; set; } = new List<INTERVENTION>();
}
