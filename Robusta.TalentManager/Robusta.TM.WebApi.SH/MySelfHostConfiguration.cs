using System.ServiceModel.Channels;
using System.Web.Http.SelfHost;
using System.Web.Http.SelfHost.Channels;

namespace Robusta.TM.WebApi.SH
{
    public class MySelfHostConfiguration : HttpSelfHostConfiguration
    {
        public MySelfHostConfiguration(string baseAddress) : base(baseAddress) { }

        protected override BindingParameterCollection OnConfigureBinding(HttpBinding httpBinding)
        {
            httpBinding.Security.Mode = HttpBindingSecurityMode.Transport;

            return base.OnConfigureBinding(httpBinding);
        }
    }
}
