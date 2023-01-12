using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ShoppingCart.Models;
using Microsoft.IdentityModel.Tokens;
using Authenticate;

namespace ShoppingCart.Services;

public class TokenService
{
    public TokenBody GenerateToken(UserData user, UserAuthenticate userAuthenticate)
    {
        //dotnet add package Microsoft.AspNetCore.Authentication
        //dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey); 
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userAuthenticate.User), // user.identity.name
                new Claim(ClaimTypes.NameIdentifier, userAuthenticate.Id.ToString()), // user.identity.name
                new Claim(ClaimTypes.Role, userAuthenticate.Role) // user.identity.isinrole
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);

        var tokenBody = new TokenBody();
        tokenBody.Token = tokenString;

        return tokenBody;
    }
}