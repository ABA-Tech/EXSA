using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class REF_STATUT_VEHICULE
{
    public string CODE { get; set; } = null!;

    public string LIBELLE { get; set; } = null!;

    public string? COULEUR_HEX { get; set; }

    public byte ORDRE { get; set; }

    public virtual ICollection<VEHICULE> VEHICULEs { get; set; } = new List<VEHICULE>();
}
