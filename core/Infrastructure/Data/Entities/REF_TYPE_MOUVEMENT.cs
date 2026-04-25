using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_TYPE_MOUVEMENT
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public virtual ICollection<MOUVEMENT_STOCK> MOUVEMENT_STOCKs { get; set; } = new List<MOUVEMENT_STOCK>();
}
