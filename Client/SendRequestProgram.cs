using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class SendRequestProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================");
            Console.WriteLine("Starting Round Robin");
            var tasks = new MakeRequest().StartRoundRobin();
            Task.WaitAll(tasks);
            foreach(var task in tasks)
            {
                Console.WriteLine(task.Result);
            }

            Console.WriteLine("Ending Round Robin");
            Console.WriteLine("====================");
        }
    }

    public class MakeRequest
    {
        public Task<string>[] StartRoundRobin()
        {
            return new[] { Get("http://localhost:8000"),
                            Get("http://localhost:8000/error"),
                            Get("http://localhost:8000/info"),
                            Get("http://localhost:8000/error/important"),
                            Get("http://localhost:8000/error/service"),
                            Get("http://localhost:8000/error/web")
            };
        }

        public async Task<string> Get(string uri)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
