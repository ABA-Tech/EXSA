namespace Domain.Models.Dto
{
    public class RationTransportGridDto
    {
        public List<TechnicienColumnDto> Techniciens { get; set; } = new();
        public List<JourRationTransportDto> Jours { get; set; } = new();
        public List<SousTotalTechnicienDto> SousTotauxTechniciens { get; set; } = new();

        public decimal GrandTotal { get; set; }
        public string Devise { get; set; } = "XAF";
    }

    public class TechnicienColumnDto
    {
        public Guid IdEmploye { get; set; }
        public string NomComplet { get; set; } = string.Empty;
        public string NumeroEmploye { get; set; } = string.Empty;
        public bool EstPrincipal { get; set; }

        public string Initiales { get; set; } = string.Empty;
    }

    public class JourRationTransportDto
    {
        public DateTime Date { get; set; }
        public string LibelleJour { get; set; } = string.Empty; // Lun, Mar, etc.
        public string LibelleDate { get; set; } = string.Empty; // 4 mai, 5 mai, etc.

        public List<CelluleRationTransportDto> Cellules { get; set; } = new();

        public decimal SousTotalRationsJour { get; set; }
        public decimal SousTotalTransportJour { get; set; }
        public decimal SousTotalJour { get; set; }
    }

    public class CelluleRationTransportDto
    {
        public Guid IdEmploye { get; set; }

        public decimal? MontantRation { get; set; }
        public decimal? MontantTransport { get; set; }

        public Guid? IdDepense { get; set; }
        public Guid? IdDepenseRation { get; set; }
        public Guid? IdDepenseTransport { get; set; }

        public decimal TotalCase { get; set; }

        public bool HasRation => MontantRation.HasValue;
        public bool HasTransport => MontantTransport.HasValue;
    }

    public class SousTotalTechnicienDto
    {
        public Guid IdEmploye { get; set; }

        public decimal SousTotalRations { get; set; }
        public decimal SousTotalTransport { get; set; }
        public decimal SousTotal { get; set; }

        public int NbJoursAvecRation { get; set; }
        public int NbJoursAvecTransport { get; set; }
    }
}
