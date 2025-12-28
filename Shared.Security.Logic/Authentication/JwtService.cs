using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Security.Encryption;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shared.Security.Authentication
{
    public class JwtService<TUser, TKey> : IJwtService<TUser, TKey>
        where TKey : IEquatable<TKey>
        where TUser : BaseUser<TKey>
    {
        private readonly JwtOptions jwtOpts;
        private readonly UserManager<TUser> userMgr;
        private readonly IRsaService rsaSrv;

        public event Action<TUser, ClaimsIdentity> AddOptionalUserClaims = (TUser user, ClaimsIdentity subject) => { };

        public JwtService(IOptions<JwtOptions> jwtOptions, UserManager<TUser> userManager, IRsaService rsaService)
        {
            jwtOpts = jwtOptions.Value;
            userMgr = userManager;
            rsaSrv = rsaService;
        }

        public async Task<string> GetTokenAsync(TKey userId, IEnumerable<Claim>? claims = null)
        {
            var user = await userMgr.FindByIdAsync(userId.ToString()!);

            if (user == null)
                throw new ApplicationException($"User with the id '{userId}' does not exist.");

            return await GetTokenAsync(user, claims);
        }

        public async Task<string> GetTokenAsync(TUser user, IEnumerable<Claim>? claims = null)
        {
            SigningCredentials credentitals = jwtOpts.Algorithm == JwtAlgorithm.RsaSha256
                ? new(new RsaSecurityKey(rsaSrv.LoadRsaKey($"{AppContext.BaseDirectory}{jwtOpts.PrivateKeyPath}")), SecurityAlgorithms.RsaSha256)
                : new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpts.SecretKey)), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity([
                    new(ClaimNames.UserId, user.Id.ToString()!),
                    new(ClaimNames.Username, user.UserName!),
                    new(ClaimNames.Email, user.Email!)
                ]),
                Issuer = jwtOpts.Issuer,
                Expires = DateTime.Now.AddSeconds(jwtOpts.DurationInSeconds),
                SigningCredentials = credentitals,
                IssuedAt = DateTime.Now
            };

            foreach (var audience in jwtOpts.Audiences)
                tokenDescriptor.Audiences.Add(audience);

            var roles = await userMgr.GetRolesAsync(user);

            if (roles.Count > 0)
                foreach (var role in roles)
                    tokenDescriptor.Subject.AddClaim(new(ClaimNames.Role, role));

            if (claims != null && claims.Any())
                tokenDescriptor.Subject.AddClaims(claims);

            AddOptionalUserClaims.Invoke(user, tokenDescriptor.Subject);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
