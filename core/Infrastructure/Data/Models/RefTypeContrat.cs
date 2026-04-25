using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class RefTypeContrat
{
    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}
