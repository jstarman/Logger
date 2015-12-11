using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace OwinApp
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
