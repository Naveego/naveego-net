using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Naveego.Json
{
    public class JsonSerializerFactory
    {

        public static JsonSerializer CreateSerializer()
        {
            var serializer = new JsonSerializer();
            serializer.ContractResolver = new NaveegoJsonContractResolver();
            serializer.Formatting = Formatting.None;
            serializer.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            serializer.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            return serializer;
        }

    }
}
