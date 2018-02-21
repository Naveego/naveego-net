using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naveego.Sync
{
    public class ResetClientMessage
    {
        [JsonProperty("reset")]
        public bool Reset { get; set; }
    }
}
