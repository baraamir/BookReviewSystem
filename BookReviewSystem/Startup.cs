using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookReviewSystem.Startup))]
namespace BookReviewSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
