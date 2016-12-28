using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Discipline.Web.Startup))]
namespace Discipline.Web {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}