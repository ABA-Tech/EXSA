namespace Domain.Models;

public class Reglement
{
    public Guid IdReglement { get; set; }
    public Guid IdFacture { get; set; }
    public decimal MontantXaf { get; set; }
    public string ModeReglement { get; set; } = null!;
    public string? ReferenceReglement { get; set; }
    public DateTime? DateReglement { get; set; }
    public DateTime? DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}