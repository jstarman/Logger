using CommandLine;
using CommandLine.Text;
using Logger.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class PushLogs
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine("====================");
                Console.WriteLine("Starting Log Push");
                var service = new LoggerService();
                service.MoveToElasticSearch(options.ShouldSave, options.LogType, options.StartDate, options.EndDate);

                Console.WriteLine("Ending Log Push");
                Console.WriteLine("====================");
            }
            else
            {
                Console.WriteLine("Exiting...bad arguments");
            }

        }

        private class Options
        {
            [Option('s', "startdate", Required = true, HelpText = "Start Date")]
            public string StartDate { get; set; }

            [Option('e', "enddate", Required = true, HelpText = "End Date")]
            public string EndDate { get; set; }

            [Option('l', "logtype", Required = true, HelpText = "End Date")]
            public string LogType { get; set; }

            [Option('r', "runelastic", Required = false, HelpText = "Save to Elastic")]
            public bool ShouldSave { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }
    }
}
