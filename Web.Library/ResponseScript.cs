using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Library
{
    public class ResponseScript
    {
        public int ResponseDelayMs { get; set; }
        public int[] ResponseCodes { get; set; }
        public dynamic Body { get; set; }
    }
}
