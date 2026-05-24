using Domain.Common.Auth.DTOs;
using Domain.Common;
using Microsoft.Extensions.Configuration;

namespace Application.Auth
{

    public sealed class AuthService : IAuthService
    {
        private readonly IUtilisateurRepository _utilisateurRepository;
        private readonly IPasswordService _passwordService;

        public AuthService(
            IUtilisateurRepository utilisateurRepository,
            IPasswordService passwordService)
        {
            _utilisateurRepository = utilisateurRepository;
            _passwordService = passwordService;
        }

        public async Task<LoginResult> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken)
        {
            var identifiant = request.Identifiant?.Trim();

            if (string.IsNullOrWhiteSpace(identifiant) ||
                string.IsNullOrWhiteSpace(request.MotDePasse))
            {
                return LoginResult.Fail(LoginFailureReason.InvalidCredentials);
            }

            var utilisateur = await _utilisateurRepository
                .GetByEmailOrTelephoneAsync(identifiant, cancellationToken);

            // Ne pas révéler si l'utilisateur existe ou non.
            if (utilisateur is null || utilisateur.EstSupprime)
            {
                return LoginResult.Fail(LoginFailureReason.InvalidCredentials);
            }

            if (utilisateur.EstActif != true)
            {
                return LoginResult.Fail(LoginFailureReason.UserDisabled);
            }

            var passwordResult = _passwordService.VerifyPassword(
                utilisateur,
                utilisateur.MotDePasseHash,
                request.MotDePasse);

            if (!passwordResult.IsValid)
            {
                return LoginResult.Fail(LoginFailureReason.InvalidCredentials);
            }

            var roleExists = await _utilisateurRepository
                .RoleExistsAsync(utilisateur.Role, cancellationToken);

            if (!roleExists)
            {
                return LoginResult.Fail(LoginFailureReason.RoleInvalid);
            }

            await _utilisateurRepository.UpdateLoginInfoAsync(
                utilisateur.IdUtilisateur,
                DateTime.UtcNow,
                request.TokenFcm,
                cancellationToken);

            if (passwordResult.NeedsRehash)
            {
                var newHash = _passwordService.HashPassword(
                    utilisateur,
                    request.MotDePasse);

                await _utilisateurRepository.UpdatePasswordHashAsync(
                    utilisateur.IdUtilisateur,
                    newHash,
                    cancellationToken);
            }

            var authenticatedUser = new AuthenticatedUser(
                utilisateur.IdUtilisateur,
                utilisateur.IdLocataire,
                utilisateur.NomComplet,
                utilisateur.Email,
                utilisateur.Telephone,
                utilisateur.Role);

            return LoginResult.Success(authenticatedUser);
        }
    }
}
