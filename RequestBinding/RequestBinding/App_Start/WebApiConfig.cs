using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Diagnostics;
using RequestBinding.Models;

namespace RequestBinding
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
            config.EnableSystemDiagnosticsTracing();
            //config.Formatters.JsonFormatter.SerializerSettings.Culture = new System.Globalization.CultureInfo("en-GB");

            config.Services.Add(typeof(System.Web.Http.ValueProviders.ValueProviderFactory), new HeaderValueProviderFactory());
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new DateTimeConverter());
            config.MessageHandlers.Add(new CultureHandler());

            foreach (var formatter in config.Formatters.Where(f => f.SupportedMediaTypes.Any(m => m.MediaType.Equals("application/x-www-form-urlencoded"))))
            {
                Trace.WriteLine(formatter.GetType().Name);
                Trace.WriteLine("\tCanReadType Employee: " + formatter.CanReadType(typeof(Employee)));
                Trace.WriteLine("\tCanWriteType Employee: " + formatter.CanWriteType(typeof(Employee)));
                Trace.WriteLine("\tCanReadType FormDataCollection: " + formatter.CanReadType(typeof(FormDataCollection)));
                Trace.WriteLine("\tCanWriteType FormDataCollection: " + formatter.CanWriteType(typeof(FormDataCollection)));
                Trace.WriteLine("\tBase: " + formatter.GetType().BaseType.Name);
                Trace.WriteLine("\tMedia Types: " + string.Join(", ", formatter.SupportedMediaTypes));
            }


            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
