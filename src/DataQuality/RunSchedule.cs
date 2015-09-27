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

namespace Naveego.DataQuality
{
    public class RunSchedule
    {
        public string Description { get; set; }

        public string Frequency { get; set; }

        public int StartHour { get; set; }

        public int StartMinute { get; set; }

        public string StartPeriod { get; set; }

        public string[] Weekdays { get; set; }

        public string CronSchedule { get; set; }
    }
}
