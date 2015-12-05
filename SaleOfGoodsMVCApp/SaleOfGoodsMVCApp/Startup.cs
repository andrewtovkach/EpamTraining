using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaleOfGoodsMVCApp.Startup))]
namespace SaleOfGoodsMVCApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
