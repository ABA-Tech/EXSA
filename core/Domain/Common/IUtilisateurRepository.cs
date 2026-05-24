using Domain.Models;

namespace Domain.Common
{
    public interface IUtilisateurRepository
    {
        Task<Utilisateur?> GetByEmailOrTelephoneAsync(
            string identifiant,
            CancellationToken cancellationToken);

        Task<Utilisateur?> GetByIdAsync(
            Guid idUtilisateur,
            CancellationToken cancellationToken);

        Task<bool> RoleExistsAsync(
            string roleCode,
            CancellationToken cancellationToken);

        Task UpdateLoginInfoAsync(
            Guid idUtilisateur,
            DateTime dateConnexionUtc,
            string? tokenFcm,
            CancellationToken cancellationToken);

        Task UpdatePasswordHashAsync(
            Guid idUtilisateur,
            string passwordHash,
            CancellationToken cancellationToken);
    }
}
