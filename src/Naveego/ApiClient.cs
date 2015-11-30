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


        public User Login(string username, string password)
        {
            var url = ToResourceUri("/user/validate");

            var loginRequest = new JObject(
                new JProperty("username", username),
                new JProperty("password", password));

            var response = ExecuteRequest<JObject>(url, new ApiRequestOptions
            {
                Method = "POST",
                Data = loginRequest
            });

            if ((bool)response["success"] == false)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var user = ((JObject)response["user"]).ToObject<User>();
            this.AuthToken = user.ServiceTicket;

            return user;
        }


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
    }
}
