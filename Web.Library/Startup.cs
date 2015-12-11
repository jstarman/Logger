using Owin;
using Nancy;
using Nancy.Owin;

namespace Web.Library
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(options => options.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound, HttpStatusCode.InternalServerError));
        }
    }
}
