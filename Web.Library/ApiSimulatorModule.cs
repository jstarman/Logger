using Nancy;
using Nancy.IO;
using Nancy.ModelBinding;
using Nancy.Responses;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Web.Library
{
    public class ApiSimulatorModule : NancyModule
    {
        private ResponseScript _responseScript;
        private static int nextIndex = 0;
        private static int maxCount;
        private static Object lockObj = new Object();

        public ApiSimulatorModule(ResponseScript responseScript)
        {
            Post["/generic", true] = async (x, ct) =>
            {
                var requestTask = GetRequest(Request.Body);
                var responseTask = Task.Delay(_responseScript.ResponseDelayMs);
                await Task.WhenAll(requestTask, responseTask);
                var response = (Response)_responseScript.Body.ToString();
                response.StatusCode = (HttpStatusCode)GetNextStatusCode();
                response.ContentType = "application/json";
                return response;
            };

            _responseScript = responseScript;
            maxCount = _responseScript.ResponseCodes.Length;
        }

        private async Task GetRequest(RequestStream stream)
        {
            var result = new byte[stream.Length];
            await stream.ReadAsync(result, 0, (int)stream.Length);
            Console.WriteLine(string.Format("Request posted Data {0}", DateTime.Now.ToString("0:MM/dd/yy H:mm:ss.fff")));
            Console.WriteLine(Encoding.ASCII.GetString(result));
            Console.WriteLine("=======================================");
        }

        private int GetNextStatusCode()
        {
            lock (lockObj)
            {
                var currentIndex = nextIndex;
                if (nextIndex == maxCount - 1)
                    nextIndex = 0;
                else
                    nextIndex++;

                return _responseScript.ResponseCodes[currentIndex];
            }
        }
    }
}
