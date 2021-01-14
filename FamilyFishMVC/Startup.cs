using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FamilyFishMVC.Startup))]
namespace FamilyFishMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
