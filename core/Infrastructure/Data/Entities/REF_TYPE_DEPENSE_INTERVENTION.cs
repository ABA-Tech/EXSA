using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_TYPE_DEPENSE_INTERVENTION
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public virtual ICollection<DEPENSE_INTERVENTION> DEPENSE_INTERVENTIONs { get; set; } = new List<DEPENSE_INTERVENTION>();
}
