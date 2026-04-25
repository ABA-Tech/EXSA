using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class RefTypeIntervention
{
    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public virtual ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
}
