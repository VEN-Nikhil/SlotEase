using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SlotEase.Application.Interfaces.Helpers;
using SlotEase.Domain;

namespace SlotEase.Application.Helpers;

public class TokenGenerator(IOptions<ConfigSettings> options, JwtSecurityTokenHandler jwtSecurityTokenHandler) : ITokenGenerator
{
    private readonly IOptions<ConfigSettings> _options = options;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = jwtSecurityTokenHandler ?? throw new ArgumentNullException(nameof(jwtSecurityTokenHandler));
    public string GenerateJwtToken(long userId, out DateTime expiryTime)
    {
        byte[] key = Encoding.UTF8.GetBytes(_options.Value.Jwt.ApiSecret);
        expiryTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_options.Value.Jwt.TokenExpiryInMinutes));

        SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(userId, key, expiryTime);
        SecurityToken token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        string tokenString = _jwtSecurityTokenHandler.WriteToken(token);
        return tokenString;

    }

    private SecurityTokenDescriptor CreateTokenDescriptor(long userId, byte[] key, DateTime expiryTime)
    {
        return new SecurityTokenDescriptor
        {
            //Subject = new ClaimsIdentity(
            //  [
            //    new("UserId", userId.ToString())
            //  ]),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = expiryTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    }
}
