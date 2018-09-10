using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VirtualExam.Startup))]
namespace VirtualExam
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
