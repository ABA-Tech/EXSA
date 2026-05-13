namespace Domain.Models.Vues;

public class VehiculeDashboard
{
    public Guid IdVehicule { get; set; }

    public string Immatriculation { get; set; } = string.Empty;

    public string Designation { get; set; } = string.Empty;

    public string TypeVehicule { get; set; } = string.Empty;

    public string Statut { get; set; } = string.Empty;

    public string StatutLibelle { get; set; } = string.Empty;

    public string? CouleurHex { get; set; }

    public int KilometrageActuel { get; set; }

    public DateTime? AssuranceExpiration { get; set; }

    public decimal TotalDepensesXaf { get; set; }

    public int EntretiensEnRetard { get; set; }

    public int EntretiensPlanifies { get; set; }

    public int ADocumentExpire { get; set; }

    public int ADocumentExpireBientot { get; set; }
}