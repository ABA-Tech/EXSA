using Domain.Common;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using PasswordVerificationResult = Domain.Common.PasswordVerificationResult;

namespace Application.Auth
{
    public sealed class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<Utilisateur> _passwordHasher = new();

        public string HashPassword(Utilisateur utilisateur, string password)
        {
            return _passwordHasher.HashPassword(utilisateur, password);
        }

        public Domain.Common.PasswordVerificationResult VerifyPassword(
            Utilisateur utilisateur,
            string passwordHash,
            string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                utilisateur,
                passwordHash,
                providedPassword);

            return result switch
            {
                Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success =>
                    new PasswordVerificationResult(true, false),

                Microsoft.AspNetCore.Identity.PasswordVerificationResult.SuccessRehashNeeded =>
                    new PasswordVerificationResult(true, true),

                _ =>
                    new PasswordVerificationResult(false, false)
            };
        }
    }
}
