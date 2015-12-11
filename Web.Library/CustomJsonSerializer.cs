using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Web.Library
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            Formatting = Formatting.Indented;
        }
    }
}
