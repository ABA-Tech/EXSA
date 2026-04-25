using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class RefTypePlan
{
    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public virtual ICollection<Locataire> Locataires { get; set; } = new List<Locataire>();
}
