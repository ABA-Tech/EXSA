using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class RefTypePhoto
{
    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public virtual ICollection<PhotoIntervention> PhotoInterventions { get; set; } = new List<PhotoIntervention>();
}
