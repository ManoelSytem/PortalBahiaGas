using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortalBahiaGas.Startup))]
namespace PortalBahiaGas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
