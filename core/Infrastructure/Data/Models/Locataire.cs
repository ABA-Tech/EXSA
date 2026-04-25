using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

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

    public virtual ICollection<ArticleStock> ArticleStocks { get; set; } = new List<ArticleStock>();

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();

    public virtual ICollection<Intervention> Interventions { get; set; } = new List<Intervention>();

    public virtual RefTypePlan TypePlanNavigation { get; set; } = null!;

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
