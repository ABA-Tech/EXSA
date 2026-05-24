using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public sealed record PasswordVerificationResult(
        bool IsValid,
        bool NeedsRehash
    );

    public interface IPasswordService
    {
        string HashPassword(Utilisateur utilisateur, string password);

        PasswordVerificationResult VerifyPassword(
            Utilisateur utilisateur,
            string passwordHash,
            string providedPassword);
    }
}
