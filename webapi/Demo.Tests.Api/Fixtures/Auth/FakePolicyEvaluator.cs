using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Demo.Tests.Api.Fixtures.Auth
{
    public class FakePolicyEvaluator : IPolicyEvaluator
    {
        private const string AuthenticationType = "FakeScheme";

        public virtual async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var principal = new ClaimsPrincipal();
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "John")
                // Add more if needed
            }, AuthenticationType);

            principal.AddIdentity(identity);

            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), AuthenticationType);
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
#nullable disable
        public virtual async Task<PolicyAuthorizationResult> AuthorizeAsync(
            AuthorizationPolicy policy,
            AuthenticateResult authenticationResult,
            HttpContext context,
            object resource) =>
            await Task.FromResult(PolicyAuthorizationResult.Success());
#nullable enable
    }
}
