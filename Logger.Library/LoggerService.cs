using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Logger.Library
{
    public class LoggerService
    {
        public void MoveToElasticSearch(bool shouldSave, string logType, string startString, string endString)
        {
            var request = new SearchRequest { 
                DateStart = DateTime.Parse(startString),
                DateEnd = DateTime.Parse(endString)                
            };

            var logs = GetLogs(logType, request);

            if (shouldSave)
            {
                SendLogToElastic(logs, logType);
            }
            else
            { 
                Console.WriteLine(JsonConvert.SerializeObject(logs.First(l => !string.IsNullOrEmpty(l.ApplicationName)), Formatting.Indented));
            }

            Console.WriteLine("{0} {1} sending count: {2}", request.DateStart.ToString(), request.DateEnd.ToString(), logs.Count().ToString());
        }

        private void SendLogToElastic(IEnumerable<LogItem> logs, string logType)
        {
            var node = new Uri("http://10.3.29.129:9200");
            var config = new ConnectionConfiguration(node);
            var elastic = new ElasticsearchClient(config);

            foreach (var item in logs)
            {
                var guid = Guid.NewGuid().ToString();
                var error = JsonConvert.SerializeObject(item);
                var response = elastic.Index("error_log", "log", guid, error);
                Console.WriteLine("Pushed {2} {0}  was successful: {1}", guid, response.SuccessOrKnownError, logType);
            }
        }

        private IEnumerable<LogItem> GetLogs(string logType, SearchRequest request)
        {
            string logCommand = string.Empty;

            switch (logType)
            {
                case "ResponseLog":
                    logCommand = ResponseLog;
                    break;
                case "RequestLog":
                    logCommand = RequestLog;
                    break;
                case "ErrorLog":
                    logCommand = ErrorLog;
                    break;
                default:
                    break;
            }

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Logging"].ConnectionString))
            {
                return conn.Query<LogItem>(logCommand, request);
            }
        }

        
    }
}
