using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Tracing;
using System.Diagnostics;
using HelloWebApi.Models;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace HelloWebApi
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

            //config.EnableSystemDiagnosticsTracing();
            //config.Services.Replace(typeof(ITraceWriter), new WebApiTracer());
            //config.MessageHandlers.Add(new TracingHandler());
            //config.Services.Replace(typeof(System.Web.Http.Tracing.ITraceWriter), new EntryExitTracer());
            //config.Formatters.RemoveAt(0);
            //config.Formatters.RemoveAt(0);
            //config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("frmt", "json", new MediaTypeHeaderValue("application/json")));
            //config.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("frmt", "xml", new MediaTypeHeaderValue("application/xml")));
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new RequestHeaderMapping("X-Media", "json", StringComparison.OrdinalIgnoreCase, false, new MediaTypeHeaderValue("application/json")));
            foreach (var formatter in config.Formatters)
            {
                Trace.WriteLine(formatter.GetType().Name);
                Trace.WriteLine("\tCanReadType: " + formatter.CanReadType(typeof(Employee)));
                Trace.WriteLine("\tCanWriteType:" + formatter.CanWriteType(typeof(Employee)));
                Trace.WriteLine("\tBase: " + formatter.GetType().BaseType.Name);
                Trace.WriteLine("\tMedia Types: " + String.Join(", ", formatter.SupportedMediaTypes));
            }

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
        }
    }
}
