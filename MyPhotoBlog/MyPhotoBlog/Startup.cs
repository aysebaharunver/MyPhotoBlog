using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPhotoBlog.Startup))]
namespace MyPhotoBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
