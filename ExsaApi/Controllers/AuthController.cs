using Domain.Common.Auth.DTOs;
using Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ExsaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAntiforgery _antiforgery;

        public AuthController(
            IAuthService authService,
            IAntiforgery antiforgery)
        {
            _authService = authService;
            _antiforgery = antiforgery;
        }

        [AllowAnonymous]
        [HttpGet("csrf")]
        public IActionResult GetCsrfToken()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);

            Response.Cookies.Append(
                "XSRF-TOKEN",
                tokens.RequestToken!,
                new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request, cancellationToken);

            if (!result.Succeeded || result.User is null)
            {
                return result.Failure switch
                {
                    LoginFailureReason.UserDisabled =>
                        Unauthorized(new { message = "Compte désactivé." }),

                    LoginFailureReason.RoleInvalid =>
                        Unauthorized(new { message = "Rôle utilisateur invalide." }),

                    _ =>
                        Unauthorized(new { message = "Identifiants invalides." })
                };
            }

            var user = result.User;

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.IdUtilisateur.ToString()),
                new("id_utilisateur", user.IdUtilisateur.ToString()),
                new("id_locataire", user.IdLocataire.ToString()),
                new(ClaimTypes.Name, user.NomComplet),
                new(ClaimTypes.Role, user.Role),
                new("role", user.Role)
            };

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties);

            return Ok(new
            {
                user.IdUtilisateur,
                user.IdLocataire,
                user.NomComplet,
                user.Email,
                user.Telephone,
                user.Role
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return NoContent();
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                IdUtilisateur = User.FindFirstValue(ClaimTypes.NameIdentifier),
                IdLocataire = User.FindFirstValue("id_locataire"),
                NomComplet = User.FindFirstValue(ClaimTypes.Name),
                Email = User.FindFirstValue(ClaimTypes.Email),
                Role = User.FindFirstValue(ClaimTypes.Role)
            });
        }

        [AllowAnonymous]
        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return Forbid();
        }
    }
}
