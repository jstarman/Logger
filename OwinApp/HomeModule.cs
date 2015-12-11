using Nancy;
using Nancy.Owin;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using log4net;
using System.Threading.Tasks;

namespace OwinApp
{
    public class HomeModule : NancyModule
    {
        private readonly ILog _logger;

        public HomeModule(ILog logger)
        {
            _logger = logger;
            Get["/"] = _ =>
            {
                _logger.Info("Hello, is anyone listening.");
                _logger.Error("A bad bug", new Exception("Just kidding it's not too bad."));
                return "Logging Error and Info" + DateTime.Now.ToString();
            };

            Get["/error"] = _ =>
            {
                new ImportantService(_logger).DoSomethingImportant();
                new LongRunningProcess(_logger).StartProcess();
                new WebRequestClient(_logger).StartRequest();
                return "Error logged" + DateTime.Now.ToString();
            };

            Get["/info"] = _ =>
            {
                _logger.Info("Hello, is anyone listening.");
                return "Info logged" + DateTime.Now.ToString();
            };

            Get["/error/important"] = _ =>
            {
                new ImportantService(_logger).DoSomethingImportant();
                return "Error ImportantService" + DateTime.Now.ToString();
            };

            Get["/error/service"] = _ =>
            {
                new LongRunningProcess(_logger).StartProcess();
                return "Error LongRunningProcess" + DateTime.Now.ToString();
            };

            Get["/error/web"] = _ =>
            {
                new WebRequestClient(_logger).StartRequest();
                return "Error WebRequestClient" + DateTime.Now.ToString();
            };

            //capture query strings http://localhost:8000/custom?blah=123
            Get["/custom"] = _ =>
            {
                return this.Request.Query["blah"];
            };

            //capture url properties http://localhost:8000/custom/Bob
            Get["/custom/{name}"] = x =>
            {
                return x.name;
            };

            /* Request body
             * { 
                    "name" : "some name",
                    "number" : 1,
                    "date" : "2015-12-08T20:57:00.0000000-08:00",
                    "list" : ["one", "two", "three" ]
                }
             */
            Post["/sendjson", true] = async (x, ct) =>
            {
                await Task.Delay(500); //contrived async wait.
                var sender = this.Bind<Sender>();
                return Response.AsJson(sender);
            };            
        }
    }

    public class Sender
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<string> List { get; set; }
    }
}
