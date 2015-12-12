using log4net;
using log4net.Config;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Library
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private const string ResponseScript = "responsescript.json";
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            XmlConfigurator.Configure();
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(typeof(ILog), (c, o) => LogManager.GetLogger(typeof(Bootstrapper)));
            container.Register<JsonSerializer, CustomJsonSerializer>();

            ResponseScript responseScript;
            if (File.Exists(ResponseScript))
                responseScript = JsonConvert.DeserializeObject<ResponseScript>(File.ReadAllText(ResponseScript));
            else
                responseScript = new ResponseScript { ResponseDelayMs = 0, ResponseCodes = new[] { 200 }, Body = { status = "OK" } };

            container.Register<ResponseScript>(responseScript);
        }
    }
}
