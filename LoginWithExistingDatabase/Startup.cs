using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginWithExistingDatabase.Startup))]
namespace LoginWithExistingDatabase
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
