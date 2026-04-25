namespace Domain.Models;

public partial class Locataire
{
    public Guid IdLocataire { get; set; }

    public string Nom { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string TypePlan { get; set; } = null!;

    public string ModulesActifs { get; set; } = null!;

    public string CodePays { get; set; } = null!;

    public string PrefixeTelephone { get; set; } = null!;

    public bool? EstActif { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }
}
