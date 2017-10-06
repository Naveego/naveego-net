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
    public class Query
    {
        public ResourceMetadata Metadata { get; set; }

        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string QueryText { get; set; }

        public string CountQueryText { get; set; }

        public string KeyColumn { get; set; }

        public string LabelColumn { get; set; }

        [JsonProperty("descColumn")]
        public string DescriptionColumn { get; set; }

        public string CountColumn { get; set; }

        public string DataOwner { get; set; }

        public RunSchedule Schedule { get; set; }

        public GuidReference Source { get; set; }

        public GuidReference Rule { get; set; }

        public JObject[] QueryProperties { get; set; }

        public JObject[] CountQueryProperties { get; set; }

        public int RunCount { get; set; }

        public DateTime? LastRun { get; set; }

        public DateTime NextRun { get; set; }

        public int ExceptionCount { get; set; }

        public DateTime? LastException { get; set; }

        public string TemplateList { get; set; }

        public VirtualCheckOverride[] VirtualOverrides { get; set; }

    }
}
