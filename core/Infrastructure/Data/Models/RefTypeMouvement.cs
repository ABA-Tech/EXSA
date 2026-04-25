using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class RefTypeMouvement
{
    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public virtual ICollection<MouvementStock> MouvementStocks { get; set; } = new List<MouvementStock>();
}
