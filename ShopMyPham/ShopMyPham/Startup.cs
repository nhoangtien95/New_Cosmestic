using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopMyPham.Startup))]
namespace ShopMyPham
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
