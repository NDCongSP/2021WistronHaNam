using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public class SummaryLogRecord
    {
        public DateTime DateTime { get; set; }
        public double? Radiation { get; set; }
        public double? Temperature { get; set; }
        public double? WindSpeed { get; set; }
        public double? Energy { get; set; }
        public double? ActivePower { get; set; }
    }
}
