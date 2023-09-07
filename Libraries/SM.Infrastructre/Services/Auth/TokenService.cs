using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using SM.Core.Interfaces.Services.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructre.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenDTO CreateToken(ApplicationUser applicationUser)
        {
            TokenDTO tokenDTO = new TokenDTO();

            tokenDTO = CreateAccessToken(tokenDTO, applicationUser);
            tokenDTO = CreateRefreshToken(tokenDTO);
            return tokenDTO;
        }
        private TokenDTO CreateAccessToken(TokenDTO tokenDTO, ApplicationUser applicationUser)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            tokenDTO.Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JWT:AccessTokenExpirationTime"]));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                audience: _configuration["JWT:Audience"],
                issuer: _configuration["JWT:Issuer"],
                expires: tokenDTO.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new Claim(ClaimTypes.Name, applicationUser.UserName),
                                          new Claim(ClaimTypes.NameIdentifier, applicationUser.Id.ToString())}
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            tokenDTO.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return tokenDTO;
        }
        private TokenDTO CreateRefreshToken(TokenDTO tokenDTO)
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            tokenDTO.RefreshToken = Convert.ToBase64String(number);
            return tokenDTO;
        }
    }
}
