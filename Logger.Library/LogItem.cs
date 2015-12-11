using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Library
{
    public class LogItem
    {
        public DateTime LoggedOn { get; set; }
        public Guid? RequestId { get; set; }
        public int? ApplicationId { get; set; }
        public string Code { get; set; }
        public string ApplicationCode { get; set; }
        public string Details { get; set; }
        public string Message { get; set; }
        public string AdditionalInfo { get; set; }
        public string RelativeUri { get; set; }
        public string Logger { get; set; }
        public string Thread { get; set; }
        public string Location { get; set; }
        public string IpAddress { get; set; }
        public string LogType { get; set; }
        public string ApplicationName { get; set; }
    }
}
