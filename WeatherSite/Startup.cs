using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeatherSite.Startup))]
namespace WeatherSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
