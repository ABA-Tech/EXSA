namespace Domain.Models.Dto
{
    public class DashboardDto
    {
        public IEnumerable<KpiDto> Kpis { get; set; }
        public IEnumerable<Alert> Alerts { get; set; }
    }

    public class KpiDto
    {
        public string Label { get; set; }
        public string Color { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
        public string Variation { get; set; }
        public string Description { get; set; }
    }

    public class Alert
    {
        public string Nom { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }
        public string Icon1 { get; set; }
        public string Icon2 { get; set; }
    }
}
