using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Implementation;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateJwtToken(User user, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF32.GetBytes(_config.GetSection("JWT:Key").Value);
        var fullnameClaim = $"{user.FirstName} {user.LastName}";
        var tokenDescriptor = new SecurityTokenDescriptor
            {
     Subject = new ClaimsIdentity(new[] 
     { 
         new Claim(JwtRegisteredClaimNames.Sub, user.Id), 
         new Claim(JwtRegisteredClaimNames.Name, fullnameClaim),
         new Claim(JwtRegisteredClaimNames.Email, user.Email),
         new Claim("role", role),
         new Claim("pfp", user.ProfilePictureUrl ?? "")
     }),
         Expires = DateTime.UtcNow.AddDays(30),
         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
         Issuer = _config.GetSection("JWT:Issuer").Value,
         Audience = _config.GetSection("JWT:Audience").Value
    };
         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
        
    }
}
