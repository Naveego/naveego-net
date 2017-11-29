using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naveego.Streams
{
    public class PagedStreamResult<T>
    {

        public int Count { get; set; }

        public long FirstId { get; set; }

        public long LastId { get; set; }

        public bool HasMore { get; set; }

        public IList<T> Events { get; set; }

    }
}
