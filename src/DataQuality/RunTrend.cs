using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naveego.DataQuality
{
    public class RunTrend
    {
        public string Type { get; set; }

        public Decimal[] Data { get; set; }

        public string[] Categories { get; set; }

        public Decimal zScore { get; set; }

        public string[] RunIds { get; set; }
    }
}
