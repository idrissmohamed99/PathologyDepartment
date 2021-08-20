using Infra.DTOs;
using Infra.Utili;
using Infra.Utili.ConfigrationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.AuthClimas
{
    public class AuthUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<AppSettingsConfig> _settings;

        public AuthUser(IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsConfig> settings)
        {
            _httpContextAccessor = httpContextAccessor;
            _settings = settings;
        }

        public async Task<string> SingIn(UserAuthDTO user)
        {
            await Task.FromResult(true);
            DateTime expired = DateTime.Now.AddHours(_settings.Value.AddHourExpired);
            DateTime issuedAt = DateTime.Now;

            ClaimsIdentity claims = addClaimsIdentity(user, expired);
            return await CreateToken(issuedAt, expired, claims);

        }

        private ClaimsIdentity addClaimsIdentity(UserAuthDTO user, DateTime expired)
        {

            ClaimsIdentity identityClaims = new ClaimsIdentity();
            identityClaims.AddClaim(new Claim(ClaimTypes.Sid, user.Id));
            identityClaims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            identityClaims.AddClaim(new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString()));
            identityClaims.AddClaim(new Claim(ClaimTypes.Expired, expired.ToString()));
            identityClaims.AddClaim(new Claim(ClaimTypes.Role, user.ModuleName));
            identityClaims.AddClaim(new Claim(ClaimTypes.Role, user.RoleName));

            foreach (string itemPermissions in user.Permisstions)
            {
                identityClaims.AddClaim(new Claim(ClaimTypes.Role, itemPermissions));
            }

            return identityClaims;

        }

        private async Task<string> CreateToken(DateTime issuedAt, DateTime expired, ClaimsIdentity identityClaims)
        {
            await Task.FromResult(true);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(_settings.Value.Secret));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidAudience = _settings.Value.ValidAudience,
                ValidIssuer = _settings.Value.ValidIssuer,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                LifetimeValidator = HelperUtili.LifetimeValidator,
                IssuerSigningKey = securityKey,
            };
            JwtSecurityToken createToken =

                   tokenHandler.CreateJwtSecurityToken(issuer: "", audience: "",
                       subject: identityClaims, notBefore: issuedAt, expires: expired, signingCredentials: signingCredentials);

            return tokenHandler.WriteToken(createToken);
        }

    }
}
