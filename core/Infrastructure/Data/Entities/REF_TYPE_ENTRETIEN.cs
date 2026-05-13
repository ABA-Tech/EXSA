using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_TYPE_ENTRETIEN
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public int? PERIODICITE_KM { get; set; }

    public byte? PERIODICITE_MOIS { get; set; }

    public virtual ICollection<ENTRETIEN_VEHICULE> ENTRETIEN_VEHICULEs { get; set; } = new List<ENTRETIEN_VEHICULE>();
}
