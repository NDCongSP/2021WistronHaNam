using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public class StationModel
    {
        public string DisplayName { get; set; } = "WISTRON INFOCOMM";
        public string Description { get; set; }
        public string Address { get; set; } = "LÔ CN-09 VÀ LÔ CN-10 KHU CÔNG NGHIỆP HỖ TRỢ ĐỒNG VĂN III, TIÊN NỘI - HOÀNG ĐÔNG - DUY TIÊN - HÀ NAM - VIỆT NAM";
        public string ConnectionStatus { get; set; } = "Online";
        public double RatedDCCapacity { get; set; } = 1000;
        public double RatedACCapacitor { get; set; } = 1000;
        public int ModuleWatt { get; set; } = 1000;
        public int TotalModules { get; set; } = 1000;
        public int TotalInverters { get; set; } = 100;

        public List<LoggerModel> Loggers { get; set; } = new List<LoggerModel>();
        public List<SensorModel> Sensors { get; set; } = new List<SensorModel>();
    }
}
