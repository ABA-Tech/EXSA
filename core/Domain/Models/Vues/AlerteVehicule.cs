namespace Domain.Models.Vues;

public class AlerteVehicule
{
    public Guid IdLocataire { get; set; }

    public Guid IdVehicule { get; set; }

    public string Immatriculation { get; set; } = string.Empty;

    public string TypeAlerte { get; set; } = string.Empty;

    public string? Message { get; set; }

    public int? JoursRetard { get; set; }

    public string Niveau { get; set; } = string.Empty;
}