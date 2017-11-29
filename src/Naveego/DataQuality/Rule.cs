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
    public class Rule
    {
        private string[] _tags;

        public ResourceMetadata Metadata { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Severity { get; set; }

        public string DataOwner { get; set; }

        public string Class { get; set; }

        public string Impact { get; set; }

        public string Category { get; set; }

        public string Object { get; set; }

        public string Property { get; set; }

        public int QueryCount { get; set; }

        public string[] Tags
        {
            get
            {
                if(_tags == null)
                {
                    _tags = new string[0];
                }
                return _tags;
            }
            set { _tags = value; }
        }

        public int RunCount { get; set; }

        public DateTime? LastRun { get; set; }

        public int ExceptionCount { get; set; }

        public DateTime? LastException { get; set; }
        
    }
}
