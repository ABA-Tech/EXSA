using Domain.Models;

namespace Domain.Models;

public partial class AffectationIntervention
{
    public Guid? IdAffectation { get; set; }

    public Guid IdIntervention { get; set; }

    public Guid IdTechnicien { get; set; }

    public DateTime DateAffectation { get; set; }

    public bool EstPrincipal { get; set; }

    public virtual Intervention? Intervention { get; set; } = null!;

    public virtual Utilisateur? Technicien { get; set; } = null!;
}
