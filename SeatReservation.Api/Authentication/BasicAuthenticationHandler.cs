using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeatReservation.Api.Models;
using SeatReservation.Api.Repositories.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SeatReservation.Api.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository userRepository;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock, IUserRepository userRepository) : base(options, logger, encoder, clock)
        {
            this.userRepository = userRepository;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Log.Information("Handling authorization for user...");
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                Log.Warning("Missing authorization header!");
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            }

            User user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                user = userRepository.Authenticate(username, password);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Invalid authorization header!");
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }

            if (user == null)
            {
                Log.Warning("Invalid username or password!");
                return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            Log.Information("User authorized successfully.");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
