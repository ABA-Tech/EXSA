using Domain.Common;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Utilisateur utilisateur)
        {
            var jwtKey = _configuration["Jwt:Key"]!;
            var issuer = _configuration["Jwt:Issuer"]!;
            var audience = _configuration["Jwt:Audience"]!;
            var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"]!);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, utilisateur.IdUtilisateur.ToString()),
            new Claim(ClaimTypes.Name, utilisateur.NomComplet),
            new Claim(ClaimTypes.Role, utilisateur.Role),
            new Claim("id_locataire", utilisateur.IdLocataire.ToString())
        };

            if (!string.IsNullOrWhiteSpace(utilisateur.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, utilisateur.Email));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
