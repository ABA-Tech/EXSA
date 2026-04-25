using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_TYPE_CONTRAT
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public virtual ICollection<EMPLOYE> EMPLOYEs { get; set; } = new List<EMPLOYE>();
}
