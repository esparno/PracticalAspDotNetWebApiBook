using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWebApi
{
    public class CultureHandler : DelegatingHandler
    {
        private ISet<String> supportedCultures = new HashSet<string>() { "en-us", "en", "fr-fr", "fr" };
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var list = request.Headers.AcceptLanguage;
            if (list != null && list.Count() >0 )
            {
                var headerValue = list.OrderByDescending(e => e.Quality ?? 1.0D)
                    .Where(e => !e.Quality.HasValue || e.Quality.Value > 0.0D)
                    .FirstOrDefault(e => supportedCultures.Contains(e.Value, StringComparer.OrdinalIgnoreCase));

                if(headerValue != null)
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(headerValue.Value);
                }

                if(list.Any(e => e.Value == "*" && (!e.Quality.HasValue || e.Quality.Value > 0.0D)))
                {
                    var culture = supportedCultures.Where(sc => list.Any(e => e.Value.Equals(sc, StringComparison.OrdinalIgnoreCase) &&
                        e.Quality.HasValue && e.Quality.Value == 0.0D)).FirstOrDefault();
                    if(culture != null)
                    {
                        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
                    }
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}