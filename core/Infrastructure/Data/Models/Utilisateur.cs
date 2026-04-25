using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

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

    public virtual ICollection<AffectationIntervention> AffectationInterventions { get; set; } = new List<AffectationIntervention>();

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();

    public virtual Locataire IdLocataireNavigation { get; set; } = null!;

    public virtual ICollection<Intervention> InterventionIdCreateurNavigations { get; set; } = new List<Intervention>();

    public virtual ICollection<Intervention> InterventionIdValidateurNavigations { get; set; } = new List<Intervention>();

    public virtual ICollection<MouvementStock> MouvementStocks { get; set; } = new List<MouvementStock>();

    public virtual ICollection<PhotoIntervention> PhotoInterventions { get; set; } = new List<PhotoIntervention>();

    public virtual ICollection<PositionGp> PositionGps { get; set; } = new List<PositionGp>();

    public virtual RefRole RoleNavigation { get; set; } = null!;
}
