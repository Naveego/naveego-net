using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naveego
{
    public class ApiRequestOptions
    {
        public string Method { get; set; }

        public bool IsAnonymous { get; set; }

        public object Data { get; set; }

        public ApiRequestOptions()
        {
            Method = "GET";
            IsAnonymous = false;
        }
    }
}
