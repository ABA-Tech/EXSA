using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class RefStatutIntervention
{
    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public byte Ordre { get; set; }

    public virtual ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();
}
