using Microsoft.IdentityModel.Tokens;
using Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API
{
    /// <summary>
    /// Crea token.
    /// </summary>
    public sealed class TokenFactory
    {
        private readonly IConfiguration _config;

        public TokenFactory(IConfiguration config)
        {
            _config = config;
        }

        public LoginResponse CreateToken(UserResponse request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            string secretKey = _config.GetValue<string>("Jwt:SecretKey")!;
            int expireTime = _config.GetValue<int>("Jwt:Expires");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secretKey);
            DateTime expires = DateTime.UtcNow.AddMinutes(expireTime);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, $"{request.idUsuario}"),
                    new Claim(ClaimTypes.Role, $"{request.idRole}")

                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginResponse
            {
                expires = expires,
                token = tokenHandler.WriteToken(token),
                idError = 0
            };
        }
    }
}
