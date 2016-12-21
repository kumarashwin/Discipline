using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tache.Web.Startup))]
namespace Tache.Web {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}