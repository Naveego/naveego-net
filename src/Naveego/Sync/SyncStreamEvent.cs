using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Naveego.Sync
{
    [JsonConverter(typeof(SyncStreamEventJsonConverter))]
    public class SyncStreamEvent
    {

        public long Id { get; set; }

        public string ContentType { get; set; }

        public Guid Key { get; set; }

        public string Action { get; set; }

        [JsonProperty("ts")]
        public DateTime Timestamp { get; set; }

        public object Content { get; set; }

    }
}
