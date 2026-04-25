namespace Domain.Models;

public partial class Employe
{
    public Guid IdEmploye { get; set; }

    public Guid IdLocataire { get; set; }

    public Guid? IdUtilisateur { get; set; }

    public string NumeroEmploye { get; set; } = null!;

    public decimal SalaireBaseXaf { get; set; }

    public string TypeContrat { get; set; } = null!;

    public DateTime DateEmbauche { get; set; }

    public string? NumeroCnps { get; set; }

    public bool? EstActif { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public virtual Locataire? IdLocataireNavigation { get; set; } = null!;

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; }

    public virtual RefTypeContrat? TypeContratNavigation { get; set; } = null!;
}
