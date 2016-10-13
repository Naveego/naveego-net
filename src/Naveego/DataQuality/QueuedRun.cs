using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Naveego.DataQuality
{
    public class QueuedRun
    {

        [JsonProperty("check_id")]
        public Guid CheckId { get; set; }

        [JsonProperty("rule_id")]
        public Guid RuleId { get; set; }

        [JsonProperty("queued_on")]
        public DateTime QueuedOn { get; set; }

        [JsonProperty("queued_by")]
        public string QueuedBy { get; set; }


    }
}
