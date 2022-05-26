using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(S2021A5SB.Startup))]

namespace S2021A5SB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
