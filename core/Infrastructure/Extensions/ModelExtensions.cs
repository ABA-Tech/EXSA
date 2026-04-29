using Infrastructure.Data.Entities;
using Domain.Models;

namespace Infrastructure.Extensions
{
    internal static class ModelExtensions
    {
        public static EMPLOYE ToEntity(this Employe employe)
        {
            return new EMPLOYE
            {
                DATE_CREATION = employe.DateCreation,
                DATE_EMBAUCHE = employe.DateEmbauche,
                DATE_MODIFICATION = employe.DateModification,
                EST_ACTIF = employe.EstActif,
                ID_EMPLOYE = employe.IdEmploye,
                ID_LOCATAIRE = employe.IdLocataire,
                ID_UTILISATEUR = employe.IdUtilisateur,
                NUMERO_CNPS = employe.NumeroCnps,
                TYPE_CONTRAT = employe.TypeContrat,
                NUMERO_EMPLOYE = employe.NumeroEmploye,
                SALAIRE_BASE_XAF = employe.SalaireBaseXaf
            };
        }

        public static Employe ToModel(this EMPLOYE entity)
        {
            return new Employe
            {
                SalaireBaseXaf = entity.SALAIRE_BASE_XAF,
                DateCreation = entity.DATE_CREATION,
                DateEmbauche = entity.DATE_EMBAUCHE,
                DateModification = entity.DATE_MODIFICATION,
                EstActif = entity.EST_ACTIF,
                IdEmploye = entity.ID_EMPLOYE,
                IdLocataire = entity.ID_LOCATAIRE,
                IdUtilisateur = entity.ID_UTILISATEUR,
                NumeroCnps = entity.NUMERO_CNPS,
                TypeContrat = entity.TYPE_CONTRAT,
                NumeroEmploye = entity.NUMERO_EMPLOYE,
                NomEmploye = entity.ID_UTILISATEURNavigation?.NOM_COMPLET,
                Utilisateur = entity.ID_UTILISATEURNavigation?.ToModel(),  
                
            };
        }

        public static IEnumerable<Employe> ToModelCollection(this IEnumerable<EMPLOYE> employeList)
        {
            return employeList.Select(x => x.ToModel());
        }

        public static Locataire ToModel(this LOCATAIRE entity)
        {
            return new Locataire
            {
                CodePays = entity.CODE_PAYS,
                DateCreation = entity.DATE_CREATION,
                DateModification = entity.DATE_MODIFICATION,
                EstActif = entity.EST_ACTIF,
                IdLocataire = entity.ID_LOCATAIRE,
                Nom = entity.NOM,
                Slug = entity.SLUG,
                PrefixeTelephone = entity.PREFIXE_TELEPHONE,
                TypePlan = entity.TYPE_PLAN,
                ModulesActifs = entity.MODULES_ACTIFS
            };
        }

        public static LOCATAIRE ToEntity(this Locataire model)
        {
            return new LOCATAIRE
            {
                CODE_PAYS = model.CodePays,
                DATE_CREATION = model.DateCreation,
                DATE_MODIFICATION = model.DateModification,
                EST_ACTIF = model.EstActif,
                ID_LOCATAIRE = model.IdLocataire,
                MODULES_ACTIFS = model.ModulesActifs,
                NOM = model.Nom,
                SLUG = model.Slug,
                PREFIXE_TELEPHONE = model.PrefixeTelephone,
                TYPE_PLAN = model.TypePlan,
            };
        }

        public static IEnumerable<Locataire> ToModelCollection(this IEnumerable<LOCATAIRE> locataireList)
        {
            return locataireList.Select(x=>x.ToModel());
        }

        public static Utilisateur ToModel(this UTILISATEUR entity)
        {
            return new Utilisateur
            {
                DateCreation = entity.DATE_CREATION,
                DateModification = entity.DATE_MODIFICATION,
                DerniereConnexion = entity.DERNIERE_CONNEXION,
                Email = entity.EMAIL,
                EstActif = entity.EST_ACTIF,
                EstSupprime = entity.EST_SUPPRIME,
                IdLocataire = entity.ID_LOCATAIRE,
                IdUtilisateur = entity.ID_UTILISATEUR,
                MotDePasseHash = entity.MOT_DE_PASSE_HASH,
                NomComplet = entity.NOM_COMPLET,
                Role = entity.ROLE,
                Telephone = entity.TELEPHONE,
                TokenFcm = entity.TOKEN_FCM
            };
        }

        public static UTILISATEUR ToEntity(this Utilisateur utilisateur)
        {
            return new UTILISATEUR
            {
                TOKEN_FCM = utilisateur.TokenFcm,
                DATE_CREATION = utilisateur.DateCreation,
                DATE_MODIFICATION = utilisateur.DateModification,
                DERNIERE_CONNEXION = utilisateur.DerniereConnexion,
                EMAIL = utilisateur.Email,
                EST_ACTIF = utilisateur.EstActif,
                EST_SUPPRIME = utilisateur.EstSupprime,
                ID_LOCATAIRE = utilisateur.IdLocataire,
                ID_UTILISATEUR = utilisateur.IdUtilisateur,
                MOT_DE_PASSE_HASH = utilisateur.MotDePasseHash,
                NOM_COMPLET = utilisateur.NomComplet,
                ROLE = utilisateur.Role,
                TELEPHONE = utilisateur.Telephone,
            };
        }
        public static IEnumerable<Utilisateur> ToModelCollection(this IEnumerable<UTILISATEUR> utilisateurs)
        {
            return utilisateurs.Select(x => x.ToModel());
        }

        public static Intervention ToModel(this INTERVENTION intervention)
        {
            return new Intervention
            {
                Adresse = intervention.ADRESSE,
                DateCreation = intervention.DATE_CREATION,
                DateModification = intervention.DATE_MODIFICATION,
                DateDebut = intervention.DATE_DEBUT,
                DateFin = intervention.DATE_FIN,
                DatePlanifiee = intervention.DATE_PLANIFIEE,
                DateValidation = intervention.DATE_VALIDATION,
                Description = intervention.DESCRIPTION,
                EstSupprime = intervention.EST_SUPPRIME,
                IdCreateur = intervention.ID_CREATEUR,
                IdIntervention = intervention.ID_INTERVENTION,
                IdLocal = intervention.ID_LOCAL,
                IdLocataire = intervention.ID_LOCATAIRE,
                IdValidateur = intervention.ID_VALIDATEUR,
                Latitude = intervention.LATITUDE,
                Longitude = intervention.LONGITUDE,
                NomClient = intervention.NOM_CLIENT,
                Notes = intervention.NOTES,
                Priorite = intervention.PRIORITE,
                Reference = intervention.REFERENCE,
                Statut = intervention.STATUT,
                Titre = intervention.TITRE,
                Type = intervention.TYPE,
                UrlSignature = intervention.URL_SIGNATURE
            };
        }

        public static INTERVENTION ToEntity(this Intervention intervention)
        {
            return new INTERVENTION
            {
                ADRESSE = intervention.Adresse,
                DATE_CREATION = intervention.DateCreation,
                DATE_DEBUT = intervention.DateDebut,
                DATE_FIN = intervention.DateFin,
                DATE_MODIFICATION = intervention.DateModification,
                DATE_PLANIFIEE = intervention.DatePlanifiee,
                DESCRIPTION = intervention.Description,
                DATE_VALIDATION = intervention.DateValidation,
                EST_SUPPRIME = intervention.EstSupprime,
                ID_CREATEUR = intervention.IdCreateur,
                ID_INTERVENTION = intervention.IdIntervention.HasValue ? intervention.IdIntervention.Value: Guid.NewGuid(),
                ID_LOCAL = intervention.IdLocal,
                ID_LOCATAIRE = intervention.IdLocataire,
                ID_VALIDATEUR = intervention.IdValidateur,
                LONGITUDE = intervention.Longitude,
                LATITUDE = intervention.Latitude,
                NOM_CLIENT = intervention.NomClient,
                NOTES = intervention.Notes,
                PRIORITE = intervention.Priorite,
                REFERENCE = intervention.Reference,
                STATUT = intervention.Statut,
                TITRE = intervention.Titre,
                TYPE = intervention.Type,
                URL_SIGNATURE = intervention.UrlSignature
            };
        }

        public static IEnumerable<Intervention> ToModelCollection(this IEnumerable<INTERVENTION> interventions)
        {
            return interventions.Select(x => x.ToModel());
        }

        public static AffectationIntervention ToModel(this AFFECTATION_INTERVENTION entity)
        {
            return new AffectationIntervention
            {
                DateAffectation = entity.DATE_AFFECTATION,
                Intervention = entity.ID_INTERVENTIONNavigation?.ToModel(),
                EstPrincipal = entity.EST_PRINCIPAL,
                IdAffectation = entity.ID_AFFECTATION,
                IdIntervention = entity.ID_INTERVENTION,
                IdTechnicien = entity.ID_TECHNICIEN,
                Technicien = entity.ID_TECHNICIENNavigation?.ToModel(),
            };
        }

        public static AFFECTATION_INTERVENTION ToEntity(this AffectationIntervention model)
        {
            return new AFFECTATION_INTERVENTION
            {
                DATE_AFFECTATION = model.DateAffectation,
                EST_PRINCIPAL = model.EstPrincipal,
                ID_INTERVENTION = model.IdIntervention,
                ID_TECHNICIEN = model.IdTechnicien,
            };
        }

        public static IEnumerable<AffectationIntervention> ToModelCollection(this IEnumerable<AFFECTATION_INTERVENTION> entities)
        {
            return entities.Select(x => x.ToModel());
        }
    }
}
