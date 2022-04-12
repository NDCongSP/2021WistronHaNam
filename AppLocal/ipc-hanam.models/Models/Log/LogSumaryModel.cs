using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public class LogSumaryModel
    {
        public string DateTime { get; set; }
        public string Radiation { get; set; } = "0";
        public string Temperature { get; set; } = "0";
        public string WindSpeed { get; set; } = "0";
        public string Energy { get; set; } = "0";
        public string ActivePower { get; set; } = "0";
    }
}
