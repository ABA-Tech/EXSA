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
    }
}
