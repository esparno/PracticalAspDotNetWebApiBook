using System.Web.Http;
using Robusta.TalentManager.WebApi.Core.Handlers;

namespace Robusta.TalentManager.WebApi.Core.Configuration
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.MessageHandlers.Add(new AuthenticationHandler());
        }
    }
}
