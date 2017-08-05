using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Robusta.TalentManager.Data;
using Robusta.TalentManager.Domain;
 
namespace Robusta.TalentManager.WebApi.Core
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private const string SCHEME = "Basic";
        private readonly IRepository<User> repository = null;

        public AuthenticationHandler(IRepository<User> repository)
        {
            this.repository = repository;
        }

        protected async override Task<HttpResponseMessage> SendAsync(
                                         HttpRequestMessage request,
                                                CancellationToken cancellationToken)
        {
            try
            {
                var headers = request.Headers;
                if (headers.Authorization != null && SCHEME.Equals(headers.Authorization.Scheme))
                {
                    Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    string credentials = encoding.GetString(
                    Convert.FromBase64String(headers.Authorization.Parameter));

                    string[] parts = credentials.Split(':');
                    string userName = parts[0].Trim();
                    string password = parts[1].Trim();

                    User user = repository.All.FirstOrDefault(u => u.UserName == userName);
                    if (user != null && user.IsAuthentic(password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userName)
                        };

                        var principal = new ClaimsPrincipal(new[] { new ClaimsIdentity(claims, SCHEME) });
                        Thread.CurrentPrincipal = principal;
                        if (HttpContext.Current != null)
                            HttpContext.Current.User = principal;
                    }
                }
                var response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(SCHEME));
                }
                return response;
            }
            catch (Exception)
            {
                var response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(SCHEME));
                return response;
            }
        }
    }
}
