using AVANADE.INFRASTRUCTURE.ServicesComum.GeradorDeIDsService;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Entidades;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices
{
    public class TokenService
    {
        private readonly JwtSettings _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public TokenService(JwtSettings jwtOptions)
        {
            _jwtOptions = jwtOptions;
            
            // Pré-configura os parâmetros de validação para reutilização
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,
                ValidateLifetime = true, // Valida a expiração por padrão
                ClockSkew = TimeSpan.Zero
            };
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = (SymmetricSecurityKey)_tokenValidationParameters.IssuerSigningKey;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(Guid IDUsuario)
        {
            return new RefreshToken
            {
                ID = CriarIDService.CriarNovoID(),
                IDUsuario = IDUsuario,
                Token = GenerateRandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays),
                Created = DateTime.UtcNow,
                IsUsed = false,
                IsRevoked = null
            };
        }
                
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = _tokenValidationParameters.Clone();         
            tokenValidationParameters.ValidateLifetime = false;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
               
                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        private static string GenerateRandomTokenString()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}