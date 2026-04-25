using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Entities;

public partial class AFFECTATION_INTERVENTION
{
    public Guid ID_AFFECTATION { get; set; }

    public Guid ID_INTERVENTION { get; set; }

    public Guid ID_TECHNICIEN { get; set; }

    public DateTime DATE_AFFECTATION { get; set; }

    public bool EST_PRINCIPAL { get; set; }

    public virtual INTERVENTION ID_INTERVENTIONNavigation { get; set; } = null!;

    public virtual UTILISATEUR ID_TECHNICIENNavigation { get; set; } = null!;
}
