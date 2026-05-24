using Domain.Models.Dto;
using Domain.Stores;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Stores;

public sealed class DashboardStore : IDashboardStore
{
    private readonly ExsaDbContext _context;

    public DashboardStore(ExsaDbContext context)
    {
        _context = context ?? throw new ApplicationException(nameof(context));
    }

    public async Task<GlobalDashboardDto> GetGlobalAsync(
        Guid idLocataire,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;
        var today = DateTime.Today;
        var debutMois = new DateTime(now.Year, now.Month, 1);
        var debutMoisSuivant = debutMois.AddMonths(1);
        var dateAlerteVehicule = today.AddDays(30);

        var dashboard = new GlobalDashboardDto();

        dashboard.Kpis = await BuildKpisAsync(
            idLocataire,
            debutMois,
            debutMoisSuivant,
            today,
            dateAlerteVehicule,
            cancellationToken);

        dashboard.InterventionsParStatut = await GetInterventionsParStatutAsync(
            idLocataire,
            cancellationToken);

        dashboard.DepensesVehiculesParType = await GetDepensesVehiculesParTypeAsync(
            idLocataire,
            debutMois,
            debutMoisSuivant,
            cancellationToken);

        dashboard.DernieresInterventions = await GetDernieresInterventionsAsync(
            idLocataire,
            cancellationToken);

        dashboard.ArticlesCritiques = await GetArticlesCritiquesAsync(
            idLocataire,
            cancellationToken);

        dashboard.AlertesVehicules = await GetAlertesVehiculesAsync(
            idLocataire,
            today,
            dateAlerteVehicule,
            cancellationToken);

        dashboard.DernieresDepensesVehicules = await GetDernieresDepensesVehiculesAsync(
            idLocataire,
            cancellationToken);

        dashboard.FacturesEnRetard = await GetFacturesEnRetardAsync(
            idLocataire,
            today,
            cancellationToken);

        return dashboard;
    }

    private async Task<DashboardKpiDto> BuildKpisAsync(
        Guid idLocataire,
        DateTime debutMois,
        DateTime debutMoisSuivant,
        DateTime today,
        DateTime dateAlerteVehicule,
        CancellationToken cancellationToken)
    {
        var interventionsQuery = _context.INTERVENTIONs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire && !x.EST_SUPPRIME);

        var articlesQuery = _context.ARTICLE_STOCKs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire);

        var vehiculesQuery = _context.VEHICULEs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire && !x.EST_SUPPRIME);

        var depensesVehiculesQuery = _context.DEPENSE_VEHICULEs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire);

        var employesQuery = _context.EMPLOYEs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire);

        var facturesQuery = _context.FACTUREs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire);

        var interventionsTotal = await interventionsQuery.CountAsync(cancellationToken);

        var interventionsEnCours = await interventionsQuery
            .CountAsync(x => x.STATUT == "EN_COURS", cancellationToken);

        var interventionsTerminees = await interventionsQuery
            .CountAsync(x =>
                x.STATUT == "TERMINEE" ||
                x.STATUT == "TERMINE" ||
                x.STATUT == "CLOTUREE" ||
                x.STATUT == "CLOTURE",
                cancellationToken);

        var interventionsOuvertes = await interventionsQuery
            .CountAsync(x =>
                x.STATUT != "TERMINEE" &&
                x.STATUT != "TERMINE" &&
                x.STATUT != "CLOTUREE" &&
                x.STATUT != "CLOTURE" &&
                x.STATUT != "ANNULEE" &&
                x.STATUT != "ANNULE",
                cancellationToken);

        var interventionsUrgentes = await interventionsQuery
            .CountAsync(x => x.PRIORITE >= 3, cancellationToken);

        var articlesTotal = await articlesQuery.CountAsync(cancellationToken);

        var articlesEnAlerte = await articlesQuery
            .CountAsync(x => x.STOCK_ACTUEL <= x.STOCK_MINIMUM, cancellationToken);

        var valeurStockXaf = await articlesQuery
            .SumAsync(x => x.STOCK_ACTUEL * (x.PRIX_UNITAIRE_XAF ?? 0m), cancellationToken);

        var vehiculesTotal = await vehiculesQuery.CountAsync(cancellationToken);

        var vehiculesAlertes = await vehiculesQuery
            .CountAsync(x =>
                (x.ASSURANCE_EXPIRATION != null &&
                 x.ASSURANCE_EXPIRATION >= today &&
                 x.ASSURANCE_EXPIRATION <= dateAlerteVehicule)
                ||
                (x.VIGNETTE_EXPIRATION != null &&
                 x.VIGNETTE_EXPIRATION >= today &&
                 x.VIGNETTE_EXPIRATION <= dateAlerteVehicule)
                ||
                (x.VISITE_TECHNIQUE_EXPIRATION != null &&
                 x.VISITE_TECHNIQUE_EXPIRATION >= today &&
                 x.VISITE_TECHNIQUE_EXPIRATION <= dateAlerteVehicule),
                cancellationToken);

        var depensesVehiculesMoisXaf = await depensesVehiculesQuery
            .Where(x => x.DATE_DEPENSE >= debutMois && x.DATE_DEPENSE < debutMoisSuivant)
            .SumAsync(x => x.MONTANT_XAF, cancellationToken);

        var employesTotal = await employesQuery.CountAsync(cancellationToken);

        var employesActifs = await employesQuery
            .CountAsync(x => x.EST_ACTIF == true, cancellationToken);

        var facturesTotal = await facturesQuery.CountAsync(cancellationToken);

        var facturesEnRetard = await facturesQuery
            .CountAsync(x =>
                x.DATE_ECHEANCE != null &&
                x.DATE_ECHEANCE < today &&
                x.STATUT != "PAYEE" &&
                x.STATUT != "ANNULEE" &&
                x.STATUT != "ANNULE",
                cancellationToken);

        var chiffreAffairesMoisXaf = await facturesQuery
            .Where(x => x.DATE_CREATION >= debutMois && x.DATE_CREATION < debutMoisSuivant)
            .SumAsync(x => x.TOTAL_XAF, cancellationToken);

        return new DashboardKpiDto
        {
            InterventionsTotal = interventionsTotal,
            InterventionsOuvertes = interventionsOuvertes,
            InterventionsEnCours = interventionsEnCours,
            InterventionsTerminees = interventionsTerminees,
            InterventionsUrgentes = interventionsUrgentes,

            ArticlesTotal = articlesTotal,
            ArticlesEnAlerte = articlesEnAlerte,
            ValeurStockXaf = valeurStockXaf,

            VehiculesTotal = vehiculesTotal,
            VehiculesAlertes = vehiculesAlertes,
            DepensesVehiculesMoisXaf = depensesVehiculesMoisXaf,

            EmployesTotal = employesTotal,
            EmployesActifs = employesActifs,

            FacturesTotal = facturesTotal,
            FacturesEnRetard = facturesEnRetard,
            ChiffreAffairesMoisXaf = chiffreAffairesMoisXaf
        };
    }

    private async Task<List<ChartItemDto>> GetInterventionsParStatutAsync(
        Guid idLocataire,
        CancellationToken cancellationToken)
    {
        return await _context.INTERVENTIONs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire && !x.EST_SUPPRIME)
            .GroupBy(x => x.STATUT)
            .Select(g => new ChartItemDto
            {
                Label = g.Key,
                Value = g.Count()
            })
            .OrderByDescending(x => x.Value)
            .ToListAsync(cancellationToken);
    }

    private async Task<List<ChartItemDto>> GetDepensesVehiculesParTypeAsync(
        Guid idLocataire,
        DateTime debutMois,
        DateTime debutMoisSuivant,
        CancellationToken cancellationToken)
    {
        return await _context.DEPENSE_VEHICULEs
            .AsNoTracking()
            .Where(x =>
                x.ID_LOCATAIRE == idLocataire &&
                x.DATE_DEPENSE >= debutMois &&
                x.DATE_DEPENSE < debutMoisSuivant)
            .GroupBy(x => x.TYPE_DEPENSE)
            .Select(g => new ChartItemDto
            {
                Label = g.Key,
                Value = g.Sum(x => x.MONTANT_XAF)
            })
            .OrderByDescending(x => x.Value)
            .Take(8)
            .ToListAsync(cancellationToken);
    }

    private async Task<List<RecentInterventionDto>> GetDernieresInterventionsAsync(
        Guid idLocataire,
        CancellationToken cancellationToken)
    {
        return await _context.INTERVENTIONs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire && !x.EST_SUPPRIME)
            .OrderByDescending(x => x.DATE_CREATION)
            .Take(8)
            .Select(x => new RecentInterventionDto
            {
                IdIntervention = x.ID_INTERVENTION,
                Reference = x.REFERENCE,
                Titre = x.TITRE,
                Description = x.DESCRIPTION,
                Statut = x.STATUT,
                Priorite = x.PRIORITE,
                NomClient = x.NOM_CLIENT,
                DatePlanifiee = x.DATE_PLANIFIEE,
                DateCreation = x.DATE_CREATION
            })
            .ToListAsync(cancellationToken);
    }

    private async Task<List<ArticleCritiqueDto>> GetArticlesCritiquesAsync(
        Guid idLocataire,
        CancellationToken cancellationToken)
    {
        return await _context.ARTICLE_STOCKs
            .AsNoTracking()
            .Where(x =>
                x.ID_LOCATAIRE == idLocataire &&
                x.STOCK_ACTUEL <= x.STOCK_MINIMUM)
            .OrderBy(x => x.STOCK_ACTUEL)
            .Take(8)
            .Select(x => new ArticleCritiqueDto
            {
                IdArticle = x.ID_ARTICLE,
                Nom = x.NOM,
                Reference = x.REFERENCE,
                Unite = x.UNITE,
                StockActuel = x.STOCK_ACTUEL,
                StockMinimum = x.STOCK_MINIMUM,
                PrixUnitaireXaf = x.PRIX_UNITAIRE_XAF
            })
            .ToListAsync(cancellationToken);
    }

    private async Task<List<VehiculeAlerteDto>> GetAlertesVehiculesAsync(
        Guid idLocataire,
        DateTime today,
        DateTime dateAlerteVehicule,
        CancellationToken cancellationToken)
    {
        return await _context.VEHICULEs
            .AsNoTracking()
            .Where(x =>
                x.ID_LOCATAIRE == idLocataire &&
                !x.EST_SUPPRIME &&
                (
                    (x.ASSURANCE_EXPIRATION != null &&
                     x.ASSURANCE_EXPIRATION >= today &&
                     x.ASSURANCE_EXPIRATION <= dateAlerteVehicule)
                    ||
                    (x.VIGNETTE_EXPIRATION != null &&
                     x.VIGNETTE_EXPIRATION >= today &&
                     x.VIGNETTE_EXPIRATION <= dateAlerteVehicule)
                    ||
                    (x.VISITE_TECHNIQUE_EXPIRATION != null &&
                     x.VISITE_TECHNIQUE_EXPIRATION >= today &&
                     x.VISITE_TECHNIQUE_EXPIRATION <= dateAlerteVehicule)
                ))
            .OrderBy(x => x.ASSURANCE_EXPIRATION)
            .Take(8)
            .Select(x => new VehiculeAlerteDto
            {
                IdVehicule = x.ID_VEHICULE,
                Immatriculation = x.IMMATRICULATION,
                Marque = x.MARQUE,
                Modele = x.MODELE,
                Statut = x.STATUT,
                AssuranceExpiration = x.ASSURANCE_EXPIRATION,
                VignetteExpiration = x.VIGNETTE_EXPIRATION,
                VisiteTechniqueExpiration = x.VISITE_TECHNIQUE_EXPIRATION
            })
            .ToListAsync(cancellationToken);
    }

    private async Task<List<RecentDepenseVehiculeDto>> GetDernieresDepensesVehiculesAsync(
        Guid idLocataire,
        CancellationToken cancellationToken)
    {
        return await _context.DEPENSE_VEHICULEs
            .AsNoTracking()
            .Where(x => x.ID_LOCATAIRE == idLocataire)
            .OrderByDescending(x => x.DATE_DEPENSE)
            .Take(8)
            .Select(x => new RecentDepenseVehiculeDto
            {
                IdDepense = x.ID_DEPENSE,
                IdVehicule = x.ID_VEHICULE,
                TypeDepense = x.TYPE_DEPENSE,
                MontantXaf = x.MONTANT_XAF,
                DateDepense = x.DATE_DEPENSE,
                Description = x.DESCRIPTION,
                VehiculeImmatriculation = x.ID_VEHICULENavigation.IMMATRICULATION
            })
            .ToListAsync(cancellationToken);
    }

    private async Task<List<FactureAlerteDto>> GetFacturesEnRetardAsync(
        Guid idLocataire,
        DateTime today,
        CancellationToken cancellationToken)
    {
        return await _context.FACTUREs
            .AsNoTracking()
            .Where(x =>
                x.ID_LOCATAIRE == idLocataire &&
                x.DATE_ECHEANCE != null &&
                x.DATE_ECHEANCE < today &&
                x.STATUT != "PAYEE" &&
                x.STATUT != "ANNULEE" &&
                x.STATUT != "ANNULE")
            .OrderBy(x => x.DATE_ECHEANCE)
            .Take(8)
            .Select(x => new FactureAlerteDto
            {
                IdFacture = x.ID_FACTURE,
                Reference = x.REFERENCE,
                NomClient = x.NOM_CLIENT,
                Statut = x.STATUT,
                TotalXaf = x.TOTAL_XAF,
                DateEcheance = x.DATE_ECHEANCE
            })
            .ToListAsync(cancellationToken);
    }
}