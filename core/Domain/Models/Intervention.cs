namespace Domain.Models;

public partial class Intervention
{
    public Guid? IdIntervention { get; set; }

    public string? IdLocal { get; set; }

    public Guid IdLocataire { get; set; }

    public string Reference { get; set; } = null!;

    public string Titre { get; set; } = null!;

    public string? Description { get; set; }

    public string Type { get; set; } = null!;

    public byte Priorite { get; set; }

    public string Statut { get; set; } = null!;

    public string? NomClient { get; set; }

    public string? Adresse { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public DateTime? DatePlanifiee { get; set; }

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public DateTime? DateValidation { get; set; }

    public Guid? IdValidateur { get; set; }

    public string? UrlSignature { get; set; }

    public string? Notes { get; set; }

    public Guid IdCreateur { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public bool EstSupprime { get; set; }

    //public virtual ICollection<AffectationIntervention> AffectationInterventions { get; set; } = new List<AffectationIntervention>();

    //public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();

    //public virtual Utilisateur IdCreateurNavigation { get; set; } = null!;

    //public virtual Locataire IdLocataireNavigation { get; set; } = null!;

    //public virtual Utilisateur? IdValidateurNavigation { get; set; }

    //public virtual ICollection<MouvementStock> MouvementStocks { get; set; } = new List<MouvementStock>();

    //public virtual ICollection<PhotoIntervention> PhotoInterventions { get; set; } = new List<PhotoIntervention>();

    //public virtual RefStatutIntervention StatutNavigation { get; set; } = null!;

    //public virtual RefTypeIntervention TypeNavigation { get; set; } = null!;
}
