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

namespace Naveego.DataQuality
{
    public class DataQualityApiClient : ApiClientBase
    {

        protected override string BasePath
        {
            get
            {
                return "dataquality";
            }
        }

        public PagedCollection<Rule> GetRules(GetCollectionOptions options = null)
        {
            options = options ?? new GetCollectionOptions();
            var resourceUri = ToResourceUri("rules");
            return ExecuteRequest<PagedCollection<Rule>>(resourceUri, new ApiRequestOptions
            {
                GetCollectionOptions = options
            });
        }

        public Rule GetRule(Guid id)
        {
            var resourceUri = ToResourceUri(string.Format("rules/{0}", id));
            return ExecuteRequest<Rule>(resourceUri);
        }

        public PagedCollection<Query> GetQueries(GetCollectionOptions options = null)
        {
            options = options ?? new GetCollectionOptions();
            var resourceUri = ToResourceUri("queries");
            return ExecuteRequest<PagedCollection<Query>>(resourceUri, new ApiRequestOptions
            {
                GetCollectionOptions = options
            });
        }

        public Query GetQuery(Guid id)
        {
            var resourceUri = ToResourceUri(string.Format("queries/{0}", id));
            return ExecuteRequest<Query>(resourceUri);
        }

    }
}
