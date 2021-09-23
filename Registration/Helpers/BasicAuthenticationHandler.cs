using Common.Models;
using EFlib.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Types;

namespace Registration.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ILogin _loginService;
        private readonly EmployeeContext m_DBContext;

        public BasicAuthenticationHandler(
           IOptionsMonitor<AuthenticationSchemeOptions> options,
           ILoggerFactory logger,
           UrlEncoder encoder,
           ISystemClock clock,
        ILogin loginService): base(options, logger, encoder, clock)
        {
            _loginService = loginService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            UserResponse user_res = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var userName = credentials[0];
                var password = credentials[1];
                user_res = await _loginService.Authenticate(userName, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user_res == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[] {
                new Claim(ClaimTypes.Email, user_res.user.Email.ToString()),
                new Claim(ClaimTypes.Name, user_res.user.EmployeeName),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
