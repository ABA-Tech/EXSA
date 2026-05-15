namespace Domain.Models
{
    public class RefTypeVehicule
    {
        public string Code { get; set; } = null!;
        public string Libelle { get; set; } = null!;
    }


    public class RefStatutVehicule
    {
        public string Code { get; set; } = null!;

        public string Libelle { get; set; } = null!;

        public string? Couleur { get; set; }

        public byte Ordre { get; set; }
    }

    public class RefTypeDepenseVehicule
    {
        public string Code { get; set; } = null!;

        public string Libelle { get; set; } = null!;

        public bool? EstConductible { get; set; }

        public string? Icone { get; set; }
    }
}
