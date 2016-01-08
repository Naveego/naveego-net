using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Naveego.Sync
{
    public class SyncClientDeploymentSettings
    {

        public string Id { get; set; }

        public string Repository { get; set; }

        public string ApiUrl { get; set; }

        public string LiveUrl { get; set; }

        public Guid SyncClientId { get; set; }

        public string DeployAuthToken { get; set; }

        public string SyncAuthToken { get; set; }

    }
}
