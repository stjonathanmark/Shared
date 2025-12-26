using Microsoft.Extensions.Options;

namespace Shared.Security.Authentication
{
    public class JwtService
    {
        private readonly JwtOptions jwtOpts;
        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            jwtOpts = jwtOptions.Value;
        }
    }
}
