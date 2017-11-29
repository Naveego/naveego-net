using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naveego.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Naveego.Sync
{
    public class WriteBack
    {
        private string[] _requiredProps;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DataSourceName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public WriteBackStatus Status { get; set; }

        public Guid DataSourceId { get; set; }

        public Guid? SyncClientId { get; set; }

        public string RunAt { get; set; }

        public string Description { get; set; }

        public string Object { get; set; }

        public string ActionType { get; set; }

        public int Queued { get; set; }

        public WriteBackRule[] Rules { get; set; }

        public string[] RequiredProperties
        {
            get
            {
                if (_requiredProps == null)
                {
                    _requiredProps = new string[0];
                }
                return _requiredProps;
            }
            set { _requiredProps = value; }
        }

        public JObject Options { get; set; }

    }
}
