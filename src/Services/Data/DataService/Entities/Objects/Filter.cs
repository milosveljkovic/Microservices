using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Entities.Objects
{
    public class Filter
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int PM25 { get; set; }
        public int PM10 { get; set; }
        public int SO2 { get; set; }
        public int NO2 { get; set; }
        public int CO { get; set; }
        public int O3 { get; set; }
    }
}
