/* Copyright 2015 Naveego Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naveego.DataQuality;
using Naveego.Security;
using Naveego.Streams;
using Naveego.Sync;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Naveego
{
    public class ApiClient : ApiClientBase
    {

       


        public PagedCollection<Connection> GetConnections(GetCollectionOptions options = null)
        {
            options = options ?? new GetCollectionOptions();
            var resourceUri = ToResourceUri("connections");
            return ExecuteRequest<PagedCollection<Connection>>(resourceUri, new ApiRequestOptions
            {
                GetCollectionOptions = options
            });
        }

        public Connection GetConnection(Guid id)
        {
            var resourceUri = ToResourceUri(string.Format("connections/{0}", id));
            return ExecuteRequest<Connection>(resourceUri);
        }

        public PagedCollection<Rule> GetRules(GetCollectionOptions options = null)
        {
            options = options ?? new GetCollectionOptions();
            var resourceUri = ToResourceUri("dataquality/rules");
            return ExecuteRequest<PagedCollection<Rule>>(resourceUri, new ApiRequestOptions
            {
                GetCollectionOptions = options
            });
        }

        public IList<WriteBack> GetWriteBacks()
        {
            var resourceUri = ToResourceUri("sync/writebacks");
            var result = ExecuteRequest<PagedCollection<WriteBack>>(resourceUri, new ApiRequestOptions {
                GetCollectionOptions = new GetCollectionOptions {  PageSize = 100000 }
            });
            return result.Data.ToList();
        }

        public IList<Dimension> GetDimensions()
        {
            var resourceUri = ToResourceUri("dataquality/dimensions");
            var result = ExecuteRequest<NonPagedList<Dimension>>(resourceUri);
            return result.Data;
        }

        public IList<WriteBackData> DequeueWritebacks(WriteBack writeBack)
        {
            var resourceUri = ToResourceUri(string.Format("sync/writebacks/{0}/queued", writeBack.Id));
            var result = ExecuteRequest<NonPagedList<WriteBackData>>(resourceUri, new ApiRequestOptions { });
            return result.Data;
        }

        public void MarkDelivered(WriteBackData writeBackData)
        {
            MarkDelievered(writeBackData);
        }
        
        [Obsolete]
        public void MarkDelievered(WriteBackData writeBackData)
        {
            var resourceUri = ToResourceUri(string.Format("sync/writebacks/{0}/queued", writeBackData.WritebackId));
            ExecuteRequest<SyncClient>(resourceUri, new ApiRequestOptions
            {
                Method = "POST",
                Data = new JObject(new JProperty("id", writeBackData.Id))
            });
        }
        
        public void MarkFailed(WriteBackData writeBackData, string reason)
        {
            var resourceUri = ToResourceUri($"sync/writebacks/{writeBackData.WritebackId}/queued/{writeBackData.Id}/failure");
            ExecuteRequest<SyncClient>(resourceUri, new ApiRequestOptions
            {
                Method = "POST",
                Data = new JObject(new JProperty("reason", reason))
            });
        }

        public Rule GetRule(Guid id)
        {
            var resourceUri = ToResourceUri(string.Format("dataquality/rules/{0}", id));
            return ExecuteRequest<Rule>(resourceUri);
        }

        public PagedCollection<Query> GetQueries(GetCollectionOptions options = null)
        {
            options = options ?? new GetCollectionOptions();
            var resourceUri = ToResourceUri("dataquality/queries");
            return ExecuteRequest<PagedCollection<Query>>(resourceUri, new ApiRequestOptions
            {
                GetCollectionOptions = options
            });
        }

        public Query GetQuery(Guid id)
        {
            var resourceUri = ToResourceUri(string.Format("dataquality/queries/{0}", id));
            return ExecuteRequest<Query>(resourceUri);
        }

        public PagedCollection<SyncClient> GetSyncClients(GetCollectionOptions options = null)
        {
            options = options ?? new GetCollectionOptions();
            var resouceUri = ToResourceUri("sync/clients");
            return ExecuteRequest<PagedCollection<SyncClient>>(resouceUri, new ApiRequestOptions
            {
                GetCollectionOptions = options
            });
        }

        public SyncClient GetSyncClient(string idOrName)
        {
            var resourceUri = ToResourceUri(string.Format("sync/clients/{0}", idOrName));
            return ExecuteRequest<SyncClient>(resourceUri);
        }

        public SyncClient AddSyncClient(SyncClient syncClient)
        {
            var resourceUri = ToResourceUri("sync/clients");
            return ExecuteRequest<SyncClient>(resourceUri, new ApiRequestOptions
            {
                Method = "POST",
                Data = syncClient
            });
        }

        public SyncClient UpdateSyncClient(SyncClient syncClient)
        {
            var resourceUri = ToResourceUri(string.Format("sync/clients/{0}", syncClient.Id));
            return ExecuteRequest<SyncClient>(resourceUri, new ApiRequestOptions
            {
                Method = "PUT",
                Data = syncClient
            });
        }

        public AuthToken GetConnectionAuthToken(Guid connectionId)
        {
            var resourceUri = ToResourceUri(string.Format("connection/{0}/authtoken", connectionId));
            var response = ExecuteRequest<JObject>(resourceUri);
            var tokenStr = (string)response["token"]["jwt"];
            return AuthToken.Parse(tokenStr);
        }

        public void DeleteSyncClient(SyncClient syncClient)
        {
            var resourceUri = ToResourceUri(string.Format("sync/clients/{0}", syncClient.Id));
            ExecuteRequest<SyncClient>(resourceUri, new ApiRequestOptions
            {
                Method = "DELETE"
            });
        }

        public PagedStreamResult<SyncStreamEvent> ReadSyncStream(long? start)
        {
            var resourceUri = ToResourceUri("/streams/sync");

            if (start != null)
            {
                resourceUri += "?start=" + start.ToString();
            }
            else
            {
                resourceUri += "?start=0";
            }

            return ExecuteRequest<PagedStreamResult<SyncStreamEvent>>(resourceUri);
        }

        public StreamWindow GetSyncStreamWindow()
        {
            var resourceUri = ToResourceUri("/streamwindows/sync");
            return ExecuteRequest<StreamWindow>(resourceUri);
        }

        public SyncClientDeploymentSettings GetDeploymentSettings(string token)
        {
            var uri = new Uri(this.ApiUrl);
            var host = uri.Host;
            var scheme = uri.Scheme;
            var port = uri.Port;
            var resourceUri = string.Empty;

            if (uri.IsDefaultPort == false)
            {
                resourceUri = string.Format("{0}://{1}:{2}/deployment/{3}", scheme, host, port, token);
            }
            else
            {
                resourceUri = string.Format("{0}://{1}/deployment/{2}", scheme, host, token);
            }

            return ExecuteRequest<SyncClientDeploymentSettings>(resourceUri, new ApiRequestOptions { IsAnonymous = true });
        }
    }
}
