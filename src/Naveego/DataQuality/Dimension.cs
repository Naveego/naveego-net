using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naveego.DataQuality
{
    public class Dimension
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsUserDefined { get; set; }

        public DimensionItem[] Items { get; set; }

    }
}
