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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Naveego.DataQuality
{
    public class RunException
    {
        public Guid Id { get; set; }

        [JsonProperty("ts")]
        public DateTime Timestamp { get; set; }

        public DateTime RunStartedAt { get; set; }

        public string Key { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public JObject Data { get; set; }

        public GuidReference Query { get; set; }

        public GuidReference Run { get; set; }

        public GuidReference Source { get; set; }

        public GuidReference SyncClient { get; set; }

        public GuidReference Rule { get; set; }

    }
}
