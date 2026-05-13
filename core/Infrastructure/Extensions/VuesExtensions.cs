using Domain.Models.Vues;
using Infrastructure.Data.Entities;

namespace Infrastructure.Extensions
{
    internal static class VuesExtensions
    {
        public static DepenseParVehicule ToModel(this V_DEPENSES_PAR_VEHICULE entity)
        {
            return new DepenseParVehicule
            {
                IdDepense = entity.ID_DEPENSE,
                IdLocataire = entity.ID_LOCATAIRE,
                Immatriculation = entity.IMMATRICULATION,
                Vehicule = entity.VEHICULE,
                TypeDepense = entity.TYPE_DEPENSE,
                TypeDepenseLibelle = entity.TYPE_DEPENSE_LIBELLE,
                EstDeductible = entity.EST_DEDUCTIBLE,
                MontantXaf = entity.MONTANT_XAF,
                DateDepense = entity.DATE_DEPENSE,
                Description = entity.DESCRIPTION,
                KilometrageAuMoment = entity.KILOMETRAGE_AU_MOMENT,
                UrlJustificatif = entity.URL_JUSTIFICATIF,
                IdIntervention = entity.ID_INTERVENTION,
                InterventionReference = entity.INTERVENTION_REFERENCE,
                InterventionTitre = entity.INTERVENTION_TITRE,
                Contexte = entity.CONTEXTE,
                SaisiePar = entity.SAISIE_PAR,
                DateCreation = entity.DATE_CREATION
            };
        }

        public static IEnumerable<DepenseParVehicule> ToModelCollection(
            this IEnumerable<V_DEPENSES_PAR_VEHICULE> entities)
        {
            return entities.Select(x => x.ToModel());
        }

        public static AlerteVehicule ToModel(this V_ALERTES_VEHICULE entity)
        {
            return new AlerteVehicule
            {
                IdLocataire = entity.ID_LOCATAIRE,
                IdVehicule = entity.ID_VEHICULE,
                Immatriculation = entity.IMMATRICULATION,
                TypeAlerte = entity.TYPE_ALERTE,
                Message = entity.MESSAGE,
                JoursRetard = entity.JOURS_RETARD,
                Niveau = entity.NIVEAU
            };
        }

        public static IEnumerable<AlerteVehicule> ToModelCollection(
            this IEnumerable<V_ALERTES_VEHICULE> entities)
        {
            return entities.Select(x => x.ToModel());
        }

        public static VehiculeDashboard ToModel(this V_VEHICULE_DASHBOARD entity)
        {
            return new VehiculeDashboard
            {
                IdVehicule = entity.ID_VEHICULE,
                Immatriculation = entity.IMMATRICULATION,
                Designation = entity.DESIGNATION,
                TypeVehicule = entity.TYPE_VEHICULE,
                Statut = entity.STATUT,
                StatutLibelle = entity.STATUT_LIBELLE,
                CouleurHex = entity.COULEUR_HEX,
                KilometrageActuel = entity.KILOMETRAGE_ACTUEL,
                AssuranceExpiration = entity.ASSURANCE_EXPIRATION,
                TotalDepensesXaf = entity.TOTAL_DEPENSES_XAF,
                EntretiensEnRetard = entity.ENTRETIENS_EN_RETARD,
                EntretiensPlanifies = entity.ENTRETIENS_PLANIFIES,
                ADocumentExpire = entity.A_DOCUMENT_EXPIRE,
                ADocumentExpireBientot = entity.A_DOCUMENT_EXPIRE_BIENTOT
            };
        }

        public static IEnumerable<VehiculeDashboard> ToModelCollection(
            this IEnumerable<V_VEHICULE_DASHBOARD> entities)
        {
            return entities.Select(x => x.ToModel());
        }

    }
}
