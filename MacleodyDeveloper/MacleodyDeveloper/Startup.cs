using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MacleodyDeveloper.Startup))]
namespace MacleodyDeveloper
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
