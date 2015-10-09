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

namespace Naveego.Sync
{
    public class SyncClient
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Host { get; set; }

        public string Type { get; set; }

        public DateTime InstallDate { get; set; }

        public string Repository { get; set; }

        public string AuthToken { get; set; }

        public string ApiUrl { get; set; }

        public string LiveUrl { get; set; }

        public string TimeZone { get; set; }

    }
}
