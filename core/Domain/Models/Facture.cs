using System.Xml.Linq;

namespace Domain.Models;

public class Facture
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

    public ICollection<LigneFacture> LigneFactures { get; set; } = new List<LigneFacture>();
    public ICollection<Reglement> Reglements { get; set; } = new List<Reglement>();
}