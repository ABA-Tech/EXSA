
namespace Domain.Models;

public partial class Utilisateur
{
    public Guid IdUtilisateur { get; set; }

    public Guid IdLocataire { get; set; }

    public string NomComplet { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string? Email { get; set; }

    public string MotDePasseHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? TokenFcm { get; set; }

    public bool? EstActif { get; set; }

    public DateTime? DerniereConnexion { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public bool EstSupprime { get; set; } 
}
