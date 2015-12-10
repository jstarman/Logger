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
            var url = "http://+:8000";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }

        private class Options
        {
            [Option('k', "publicKey", Required = false, HelpText = "Public key file name")]
            public string PublicKeyFileName { get; set; }

            [Option('s', "secretkey", Required = false, HelpText = "Private key file name")]
            public string PrivateKeyFileName { get; set; }

            [Option('p', "password", Required = false, HelpText = "Private key password")]
            public string PrivateKeyPassword { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }
    }
}