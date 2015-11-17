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
using Newtonsoft.Json.Linq;

namespace Naveego
{
    public class Connection
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string RunAt { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public BusinessApplicationRef Application { get; set; }

        public DateTime? StatusDate { get; set; }
        
        public Reference SyncClient { get; set; }

        public JObject Settings { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}
