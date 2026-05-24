using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Auth.DTOs
{

    public sealed record LoginRequest(
        string Identifiant,
        string MotDePasse,
        string? TokenFcm
    );

    public sealed record AuthenticatedUser(
        Guid IdUtilisateur,
        Guid IdLocataire,
        string NomComplet,
        string? Email,
        string Telephone,
        string Role
    );

    public enum LoginFailureReason
    {
        InvalidCredentials,
        UserDisabled,
        RoleInvalid
    }

    public sealed record LoginResult(
        bool Succeeded,
        AuthenticatedUser? User,
        LoginFailureReason? Failure
    )
    {
        public static LoginResult Success(AuthenticatedUser user)
            => new(true, user, null);

        public static LoginResult Fail(LoginFailureReason reason)
            => new(false, null, reason);
    }
}
