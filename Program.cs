using CommandLine;
using CommandLine.Text;
using Microsoft.Owin.Hosting;
using System;
using OwinApp;


namespace Logger
{
    class LoggerProgram
    {
        private static void Main(string[] args)
        {
            var options = new Options();
            if (Parser.Default.ParseArguments(args, options))
            {
                var url = options.Url;

                using (WebApp.Start<Startup>(url))
                {
                    Console.WriteLine("Running on {0}", url);
                    Console.WriteLine("Press enter to exit");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Exiting...bad arguments");
            }
        }

        private class Options
        {
            [Option('u', "url", Required = false, DefaultValue="http://+:8000", HelpText = "Root url")]
            public string Url { get; set; }

            [Option('s', "script", Required = false, HelpText = "Response script file name to run")]
            public string Script { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }
    }
}