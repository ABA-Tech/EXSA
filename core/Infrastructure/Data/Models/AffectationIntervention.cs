using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class AffectationIntervention
{
    public Guid IdAffectation { get; set; }

    public Guid IdIntervention { get; set; }

    public Guid IdTechnicien { get; set; }

    public DateTime DateAffectation { get; set; }

    public bool EstPrincipal { get; set; }

    public virtual Intervention IdInterventionNavigation { get; set; } = null!;

    public virtual Utilisateur IdTechnicienNavigation { get; set; } = null!;
}
