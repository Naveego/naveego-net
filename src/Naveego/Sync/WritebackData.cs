using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Naveego.Sync
{
    public class WriteBackData
    {
        public string Id { get; set; }

        public string Writeback { get; set; }

        public Guid WritebackId { get; set; }

        public Guid GlobalId { get; set; }

        public string DataSourceName { get; set; }

        public string Object { get; set; }

        public string ObjectDesc { get; set; }

        public string Source { get; set; }

        public string SourceId { get; set; }

        public JObject Metadata { get; set; }

        public JObject Data { get; set; }

    }
}
