using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Models;

public partial class Facture
{
    public Guid IdFacture { get; set; }

    public Guid IdLocataire { get; set; }

    public Guid? IdIntervention { get; set; }

    public string Reference { get; set; } = null!;

    public string Statut { get; set; } = null!;

    public string NomClient { get; set; } = null!;

    public decimal SousTotalXaf { get; set; }

    public decimal TauxTva { get; set; }

    public decimal TotalXaf { get; set; }

    public DateTime? DateEcheance { get; set; }

    public DateTime? DatePaiement { get; set; }

    public string? UrlPdf { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public virtual Intervention? IdInterventionNavigation { get; set; }

    public virtual Locataire IdLocataireNavigation { get; set; } = null!;

    public virtual ICollection<LigneFacture> LigneFactures { get; set; } = new List<LigneFacture>();

    public virtual RefStatutFacture StatutNavigation { get; set; } = null!;
}
