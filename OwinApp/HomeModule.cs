using Nancy;
using Nancy.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using log4net;

namespace OwinApp
{
    public class HomeModule : NancyModule
    {
        private readonly ILog _logger;

        public HomeModule(ILog logger)
        {
            _logger = logger;
            Get["/"] = x =>
            {
                _logger.Info("Hello, is anyone listening.");
                _logger.Error("A bad bug", new Exception("Just kidding it's not too bad."));
                return "Logging Error and Info" + DateTime.Now.ToString();
            };

            Get["/error"] = x =>
            {
                new ImportantService(_logger).DoSomethingImportant();
                new LongRunningProcess(_logger).StartProcess();
                new WebRequestClient(_logger).StartRequest();
                return "Error logged" + DateTime.Now.ToString();
            };

            Get["/info"] = x =>
            {
                _logger.Info("Hello, is anyone listening.");
                return "Info logged" + DateTime.Now.ToString();
            };

            Get["/error/important"] = x =>
            {
                new ImportantService(_logger).DoSomethingImportant();
                return "Error ImportantService" + DateTime.Now.ToString();
            };

            Get["/error/service"] = x =>
            {
                new LongRunningProcess(_logger).StartProcess();
                return "Error LongRunningProcess" + DateTime.Now.ToString();
            };

            Get["/error/web"] = x =>
            {
                new WebRequestClient(_logger).StartRequest();
                return "Error WebRequestClient" + DateTime.Now.ToString();
            };
        }
    }
}
