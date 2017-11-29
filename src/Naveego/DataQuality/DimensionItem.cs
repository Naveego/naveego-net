using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naveego.DataQuality
{
    public class DimensionItem
    {
        public string ItemId { get; set; }

        public string Label { get; set; }

        public string DefaultValue { get; set; }

        public DimensionSourceValue[] SourceValues { get; set; }
    }
}
