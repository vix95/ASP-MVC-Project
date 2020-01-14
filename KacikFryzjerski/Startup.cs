using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KacikFryzjerski.Startup))]
namespace KacikFryzjerski
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
