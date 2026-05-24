using Domain.Common;
using Domain.Models;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public sealed class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly ExsaDbContext _dbContext;

        public UtilisateurRepository(ExsaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Utilisateur?> GetByEmailOrTelephoneAsync(
            string identifiant,
            CancellationToken cancellationToken)
        {
            var normalized = identifiant.Trim().ToLower();

            return await _dbContext.UTILISATEURs
                .AsNoTracking()
                .Where(u =>
                    !u.EST_SUPPRIME &&
                    (
                        u.EMAIL != null && u.EMAIL.ToLower() == normalized ||
                        u.TELEPHONE == identifiant
                    ))
                .Select(u => new Utilisateur
                {
                    IdUtilisateur = u.ID_UTILISATEUR,
                    IdLocataire = u.ID_LOCATAIRE,
                    NomComplet = u.NOM_COMPLET,
                    Telephone = u.TELEPHONE,
                    Email = u.EMAIL,
                    MotDePasseHash = u.MOT_DE_PASSE_HASH,
                    Role = u.ROLE,
                    TokenFcm = u.TOKEN_FCM,
                    EstActif = u.EST_ACTIF,
                    DerniereConnexion = u.DERNIERE_CONNEXION,
                    DateCreation = u.DATE_CREATION,
                    DateModification = u.DATE_MODIFICATION,
                    EstSupprime = u.EST_SUPPRIME
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Utilisateur?> GetByIdAsync(
            Guid idUtilisateur,
            CancellationToken cancellationToken)
        {
            return await _dbContext.UTILISATEURs
                .AsNoTracking()
                .Where(u => u.ID_UTILISATEUR == idUtilisateur && !u.EST_SUPPRIME)
                .Select(u => new Utilisateur
                {
                    IdUtilisateur = u.ID_UTILISATEUR,
                    IdLocataire = u.ID_LOCATAIRE,
                    NomComplet = u.NOM_COMPLET,
                    Telephone = u.TELEPHONE,
                    Email = u.EMAIL,
                    MotDePasseHash = u.MOT_DE_PASSE_HASH,
                    Role = u.ROLE,
                    TokenFcm = u.TOKEN_FCM,
                    EstActif = u.EST_ACTIF,
                    DerniereConnexion = u.DERNIERE_CONNEXION,
                    DateCreation = u.DATE_CREATION,
                    DateModification = u.DATE_MODIFICATION,
                    EstSupprime = u.EST_SUPPRIME
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> RoleExistsAsync(
            string roleCode,
            CancellationToken cancellationToken)
        {
            return await _dbContext.REF_ROLEs
                .AnyAsync(r => r.CODE == roleCode, cancellationToken);
        }

        public async Task UpdateLoginInfoAsync(
            Guid idUtilisateur,
            DateTime dateConnexionUtc,
            string? tokenFcm,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.UTILISATEURs
                .FirstAsync(u => u.ID_UTILISATEUR == idUtilisateur, cancellationToken);

            user.DERNIERE_CONNEXION = dateConnexionUtc;
            user.TOKEN_FCM = tokenFcm;
            user.DATE_MODIFICATION = dateConnexionUtc;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdatePasswordHashAsync(
            Guid idUtilisateur,
            string passwordHash,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.UTILISATEURs
                .FirstAsync(u => u.ID_UTILISATEUR == idUtilisateur, cancellationToken);

            user.MOT_DE_PASSE_HASH = passwordHash;
            user.DATE_MODIFICATION = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
