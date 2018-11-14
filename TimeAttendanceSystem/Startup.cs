using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeAttendanceSystem.Startup))]
namespace TimeAttendanceSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
