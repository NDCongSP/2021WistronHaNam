using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public class InverterLogRecord
    {
        public DateTime DateTime { get; set; }
        public double? ActivePower { get; set; }
        public double? TotalEnergyYields { get; set; }
        public double? Temperature { get; set; }
        public double? Radiation { get; set; }
    }
}
