
namespace Domain.Models;

public partial class RefStatutIntervention
{
    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public byte Ordre { get; set; }
     
}
