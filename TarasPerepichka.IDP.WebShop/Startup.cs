using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TarasPerepichka.IDP.WebShop.Startup))]
namespace TarasPerepichka.IDP.WebShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
