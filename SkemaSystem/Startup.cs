using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SkemaSystem.Startup))]
namespace SkemaSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
