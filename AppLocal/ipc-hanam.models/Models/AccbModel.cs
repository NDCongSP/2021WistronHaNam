using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public class AccbModel
    {
        public string DisplayName { get; set; }
        public int Index { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string DeviceId { get; set; }
        public string TagPrefix { get; set; }

        public List<MccbModel> Mccbs { get; set; } = new List<MccbModel>();
    }
}
