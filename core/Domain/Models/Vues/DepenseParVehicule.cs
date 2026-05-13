namespace Domain.Models.Vues;

public class DepenseParVehicule
{
    public Guid IdDepense { get; set; }

    public Guid IdLocataire { get; set; }

    public string Immatriculation { get; set; } = string.Empty;

    public string Vehicule { get; set; } = string.Empty;

    public string TypeDepense { get; set; } = string.Empty;

    public string TypeDepenseLibelle { get; set; } = string.Empty;

    public bool EstDeductible { get; set; }

    public decimal MontantXaf { get; set; }

    public DateTime DateDepense { get; set; }

    public string? Description { get; set; }

    public int? KilometrageAuMoment { get; set; }

    public string? UrlJustificatif { get; set; }

    public Guid? IdIntervention { get; set; }

    public string? InterventionReference { get; set; }

    public string? InterventionTitre { get; set; }

    public string Contexte { get; set; } = string.Empty;

    public string SaisiePar { get; set; } = string.Empty;

    public DateTime DateCreation { get; set; }
}