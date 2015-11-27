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

namespace Naveego.Security
{

    public class User 
    {

        private string[] _modules;
        private string[] _roles;

        public string Id { get; set; }

        public string Name
        {
            get { return Id; }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ServiceTicket { get; set; }

        public bool IsDisabled { get; set; }

        public bool ForcePasswordReset { get; set; }

        public bool ForceAcceptAgreement { get; set; }

        public DateTime? LastLogin { get; set; }

        public string AccessCode { get; set; }

        public int AccessLevel { get; set; }

        public string BusinessPhone { get; set; }

        public string Edition { get; set; }

        public string MobilePhone { get; set; }

        public Guid UserId { get; set; }

        public string DisplayName { get; set; }

        public string[] Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new string[0];
                }
                return _roles;
            }
            set { _roles = value; }
        }

        public string[] Modules
        {
            get
            {
                if (_modules == null)
                {
                    _modules = new string[0];
                }
                return _modules;
            }
            set { _modules = value; }
        }
      
    }
}
