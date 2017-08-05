using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Web;

namespace Robusta.TalentManager.WebApi.Core.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("X-PSK"))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "jqhuman")
            };

                var principal = new ClaimsPrincipal(new[] { new ClaimsIdentity(claims, "Basic") });

                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
