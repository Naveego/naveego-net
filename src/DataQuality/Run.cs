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
    public class Run
    {
        public ResourceMetadata Metadata { get; set; }

        public Guid Id { get; set; }

        public string Status { get; set; }

        public string Indicator { get; set; }

        public string Severity { get; set; }

        public string Impact { get; set; }

        public string Class { get; set; }

        public string Category { get; set; }

        public string Object { get; set; }

        public string Property { get; set; }

        public string AssignedTo { get; set; }

        public DateTime ScheduleStart { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public int Population { get; set; }

        public int ExceptionCount { get; set; }

        public long QueryTime { get; set; }

        public bool Acknowledged { get; set; }

        public string AcknowledgedBy { get; set; }

        public DateTime? AcknowledgedOn { get; set; }

        public string QueryText { get; set; }

        public string CountQueryText { get; set; }

        public string ErrorMessage { get; set; }

        public RunTrend[] Trends { get; set; }

        public Reference Query { get; set; }

        public Reference Rule { get; set; }

        public Reference Source { get; set; }

        public Reference SyncClient { get; set; }

    }
}
