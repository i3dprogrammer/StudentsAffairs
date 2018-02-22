using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentsAffairs.Startup))]
namespace StudentsAffairs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
