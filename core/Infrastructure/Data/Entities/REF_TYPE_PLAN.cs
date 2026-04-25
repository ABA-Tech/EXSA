using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_TYPE_PLAN
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public virtual ICollection<LOCATAIRE> LOCATAIREs { get; set; } = new List<LOCATAIRE>();
}
