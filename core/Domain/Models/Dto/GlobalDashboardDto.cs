namespace Domain.Models.Dto;

public sealed class GlobalDashboardDto
{
    public DashboardKpiDto Kpis { get; set; } = new();

    public List<ChartItemDto> InterventionsParStatut { get; set; } = new();

    public List<ChartItemDto> DepensesVehiculesParType { get; set; } = new();

    public List<RecentInterventionDto> DernieresInterventions { get; set; } = new();

    public List<ArticleCritiqueDto> ArticlesCritiques { get; set; } = new();

    public List<VehiculeAlerteDto> AlertesVehicules { get; set; } = new();

    public List<RecentDepenseVehiculeDto> DernieresDepensesVehicules { get; set; } = new();

    public List<FactureAlerteDto> FacturesEnRetard { get; set; } = new();
}

public sealed class DashboardKpiDto
{
    public int InterventionsTotal { get; set; }

    public int InterventionsOuvertes { get; set; }

    public int InterventionsEnCours { get; set; }

    public int InterventionsTerminees { get; set; }

    public int InterventionsUrgentes { get; set; }

    public int ArticlesTotal { get; set; }

    public int ArticlesEnAlerte { get; set; }

    public decimal ValeurStockXaf { get; set; }

    public int VehiculesTotal { get; set; }

    public int VehiculesAlertes { get; set; }

    public decimal DepensesVehiculesMoisXaf { get; set; }

    public int EmployesTotal { get; set; }

    public int EmployesActifs { get; set; }

    public int FacturesTotal { get; set; }

    public int FacturesEnRetard { get; set; }

    public decimal ChiffreAffairesMoisXaf { get; set; }
}

public sealed class ChartItemDto
{
    public string Label { get; set; } = null!;

    public decimal Value { get; set; }
}

public sealed class RecentInterventionDto
{
    public Guid IdIntervention { get; set; }

    public string Reference { get; set; } = null!;

    public string Titre { get; set; } = null!;

    public string? Description { get; set; }

    public string Statut { get; set; } = null!;

    public byte Priorite { get; set; }

    public string? NomClient { get; set; }

    public DateTime? DatePlanifiee { get; set; }

    public DateTime DateCreation { get; set; }
}

public sealed class ArticleCritiqueDto
{
    public Guid IdArticle { get; set; }

    public string Nom { get; set; } = null!;

    public string? Reference { get; set; }

    public string Unite { get; set; } = null!;

    public decimal StockActuel { get; set; }

    public decimal StockMinimum { get; set; }

    public decimal? PrixUnitaireXaf { get; set; }
}

public sealed class VehiculeAlerteDto
{
    public Guid IdVehicule { get; set; }

    public string Immatriculation { get; set; } = null!;

    public string Marque { get; set; } = null!;

    public string Modele { get; set; } = null!;

    public string Statut { get; set; } = null!;

    public DateTime? AssuranceExpiration { get; set; }

    public DateTime? VignetteExpiration { get; set; }

    public DateTime? VisiteTechniqueExpiration { get; set; }
}

public sealed class RecentDepenseVehiculeDto
{
    public Guid IdDepense { get; set; }

    public Guid IdVehicule { get; set; }

    public string TypeDepense { get; set; } = null!;

    public decimal MontantXaf { get; set; }

    public DateTime DateDepense { get; set; }

    public string? Description { get; set; }

    public string? VehiculeImmatriculation { get; set; }
}

public sealed class FactureAlerteDto
{
    public Guid IdFacture { get; set; }

    public string Reference { get; set; } = null!;

    public string NomClient { get; set; } = null!;

    public string Statut { get; set; } = null!;

    public decimal TotalXaf { get; set; }

    public DateTime? DateEcheance { get; set; }
}