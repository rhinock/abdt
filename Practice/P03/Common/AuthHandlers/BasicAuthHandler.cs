using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Common.AuthHandlers
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // admin:admin -> base64 -> Basic base64data...
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing header");
            }

            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var headerBytes = Convert.FromBase64String(header.Parameter);
            var credentials = Encoding.UTF8.GetString(headerBytes).Split(':', 2);

            if (credentials[0] == "admin" && credentials[1] == "admin")
            {
                var claimIdentity = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, "admin"),
                        new Claim("Role", "mainUser")
                    },
                    Scheme.Name);

                var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimIdentity), Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Incorrect login or password");
        }
    }
}
