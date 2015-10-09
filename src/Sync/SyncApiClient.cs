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
using Naveego.Streams;

namespace Naveego.Sync
{
    public class SyncApiClient : ApiClientBase
    {

        protected override string BasePath
        {
            get
            {
                return "sync";
            }
        }


        public PagedCollection<SyncClient> GetSyncClients(GetCollectionOptions options = null)
        {
            options = options ?? new GetCollectionOptions();
            var resouceUri = ToResourceUri("clients");
            return ExecuteRequest<PagedCollection<SyncClient>>(resouceUri, new ApiRequestOptions
            {
                GetCollectionOptions = options
            });
        }

        public SyncClient GetSyncClient(Guid id)
        {
            var resourceUri = ToResourceUri(string.Format("clients/{0}", id));
            return ExecuteRequest<SyncClient>(resourceUri);
        }

        public SyncClient AddSyncClient(SyncClient syncClient)
        {
            var resourceUri = ToResourceUri("clients");
            return ExecuteRequest<SyncClient>(resourceUri, new ApiRequestOptions
            {
                Method = "POST",
                Data = syncClient
            });
        }

        public SyncClient UpdateSyncClient(SyncClient syncClient)
        {
            var resourceUri = ToResourceUri(string.Format("clients/{0}", syncClient.Id));
            return ExecuteRequest<SyncClient>(resourceUri, new ApiRequestOptions
            {
                Method = "PUT",
                Data = syncClient
            });
        }

        public PagedStreamResult<SyncStreamEvent> ReadSyncStream(Guid? start)
        {
            var resourceUri = string.Format("{0}/streams/sync", ApiUrl);

            if(start != null)
            {
                resourceUri += "?start=" + start.ToString().ToLowerInvariant();
            }

            return ExecuteRequest<PagedStreamResult<SyncStreamEvent>>(resourceUri);
        }

        public StreamWindow GetSyncStreamWindow()
        {
            var resourceUri = string.Format("{0}/streamwindows/sync", ApiUrl);
            return ExecuteRequest<StreamWindow>(resourceUri);
        }


    }
}
