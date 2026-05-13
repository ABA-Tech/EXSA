using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class AFFECTATION_VEHICULE
{
    public Guid ID_AFFECTATION_VEHICULE { get; set; }

    public Guid ID_VEHICULE { get; set; }

    public Guid ID_INTERVENTION { get; set; }

    public Guid ID_CONDUCTEUR { get; set; }

    public int KILOMETRAGE_DEPART { get; set; }

    public int? KILOMETRAGE_ARRIVEE { get; set; }

    public int? DISTANCE_KM { get; set; }

    public DateTime DATE_DEBUT { get; set; }

    public DateTime? DATE_FIN { get; set; }

    public string? NOTES { get; set; }

    public DateTime DATE_CREATION { get; set; }

    public virtual UTILISATEUR ID_CONDUCTEURNavigation { get; set; } = null!;

    public virtual INTERVENTION ID_INTERVENTIONNavigation { get; set; } = null!;

    public virtual VEHICULE ID_VEHICULENavigation { get; set; } = null!;
}
