using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tache.Example.Startup))]
namespace Tache.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
