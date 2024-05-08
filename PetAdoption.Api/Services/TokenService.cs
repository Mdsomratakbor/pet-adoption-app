﻿using Microsoft.IdentityModel.Tokens;
using PetAdoption.Api.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetAdoption.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)

        {
            _configuration = configuration;
        }

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration) =>
        new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = GetSecurityKey(configuration),
        };
        public string GenerateJWT(IEnumerable<Claim> additionalClaims = null)
        {
            var securityKey = GetSecurityKey(_configuration);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireMinutes = Convert.ToInt32(_configuration["Jwt:ExpireInminutes"]??"60");
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            if(additionalClaims.Any() == true)
            {
                claims.AddRange(additionalClaims);
            }
            var token = new JwtSecurityToken(issuer: _configuration["Jwt: Issuer"],
                audience: _configuration["Jwt: Audience"], 
                claims:claims, 
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateJWT(User user, IEnumerable<Claim>? additionalClaims = null)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            if(additionalClaims?.Any() == true)
                claims.AddRange(additionalClaims);

            return GenerateJWT(claims);
        }

        private static SymmetricSecurityKey GetSecurityKey(IConfiguration _configuration) =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));


    }
}
