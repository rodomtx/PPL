using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(stoppage.Startup))]
namespace stoppage
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
