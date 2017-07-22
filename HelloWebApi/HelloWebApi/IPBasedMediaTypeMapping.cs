using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Channels;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace HelloWebApi
{
    public class IPBasedMediaTypeMapping : MediaTypeMapping
    {
        public IPBasedMediaTypeMapping() : base(new MediaTypeHeaderValue("application/json")) { }
        public override double TryMatchMediaType(HttpRequestMessage request)
        {
            string ipAddress = String.Empty;
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var httpContext = (HttpContextBase)request.Properties["MS_HttpContext"];
                ipAddress = httpContext.Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop;
                prop = (RemoteEndpointMessageProperty) request.Properties[RemoteEndpointMessageProperty.Name];
                ipAddress = prop.Address;
            }
            return "::1".Equals(ipAddress) ? 1.0 : 0.0;
        }
    }
}