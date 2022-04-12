using AFSClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public class InverterModel : INotifyPropertyChanged
    {
        private string _tagPrefix;
        private string _status;
        private string _mccbStatus;

        public string DisplayName { get; set; }
        public int Index { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string DeviceId { get; set; }
        public string MCCBStatus
        {
            get => _mccbStatus;
            set
            {
                if (_mccbStatus != value)
                {
                    _mccbStatus = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string TagPrefix
        {
            get => _tagPrefix;
            set
            {
                if (_tagPrefix != value)
                {
                    _tagPrefix = value;
                    RaisePropertyChanged();
                    this.LoadTags();
                    GetPVTags();
                    if (device_status != null)
                    {
                        device_status.QualityChanged += Device_status_QualityChanged;
                        device_status.ValueChanged += Device_status_ValueChanged;
                    }

                    if (mccb != null)
                    {
                        mccb.QualityChanged += Mccb_QualityChanged;
                        mccb.ValueChanged += Mccb_ValueChanged;
                    }

                    if (trip != null)
                    {
                        trip.QualityChanged += Mccb_QualityChanged;
                        trip.ValueChanged += Mccb_ValueChanged;
                    }

                    Device_status_ValueChanged(null, null);
                }
            }
        }
        public string HistoryPath { get; set; }
        public string AlarmPath { get; set; }
        public bool IsRunning
        {
            get
            {
                return Status != "Offline";
            }
        }

        private void Mccb_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (mccb != null && trip != null)
            {
                if (mccb.Quality == Quality.Good &&
                    trip.Quality == Quality.Good)
                {
                    if (trip.Value == "1")
                    {
                        MCCBStatus = "Trip";
                    }
                    else if (trip.Value == "0")
                    {
                        if (mccb.Value == "1")
                        {
                            MCCBStatus = "Closed";
                        }
                        else if (mccb.Value == "0")
                        {
                            MCCBStatus = "Opened";
                        }
                        else
                        {
                            MCCBStatus = "Offline";

                        }
                    }
                    else
                    {
                        MCCBStatus = "Offline";

                    }
                    return; 
                }
            }
            MCCBStatus = "Offline";
        }

        private void Mccb_QualityChanged(object sender, TagQualityChangedEventArgs e)
        {
            Mccb_ValueChanged(null, null);
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(IsRunning));
                }
            }
        }

        public int StringCount { get; set; } = 10;

        public TagNode daily_energy_yields { get; set; }
        public TagNode total_energy_yields { get; set; }
        public TagNode ab_line_voltage { get; set; }
        public TagNode bc_line_voltage { get; set; }
        public TagNode ca_line_voltage { get; set; }
        public TagNode phase_a_voltage { get; set; }
        public TagNode phase_b_voltage { get; set; }
        public TagNode phase_c_voltage { get; set; }
        public TagNode phase_a_current { get; set; }
        public TagNode phase_b_current { get; set; }
        public TagNode phase_c_current { get; set; }
        public TagNode active_power { get; set; }
        public TagNode reactive_power { get; set; }
        public TagNode power_factor { get; set; }
        public TagNode grid_frequency { get; set; }
        public TagNode effciency { get; set; }
        public TagNode temperature { get; set; }
        [Subscribe]
        public TagNode device_status { get; set; }
        public TagNode alarm1 { get; set; }
        public TagNode alarm2 { get; set; }
        public TagNode alarm3 { get; set; }
        public TagNode pv1_voltage { get; set; }
        public TagNode pv1_current { get; set; }
        public TagNode pv2_voltage { get; set; }
        public TagNode pv2_current { get; set; }
        public TagNode pv3_voltage { get; set; }
        public TagNode pv3_current { get; set; }
        public TagNode pv4_voltage { get; set; }
        public TagNode pv4_current { get; set; }
        public TagNode pv5_voltage { get; set; }
        public TagNode pv5_current { get; set; }
        public TagNode pv6_voltage { get; set; }
        public TagNode pv6_current { get; set; }
        public TagNode pv7_voltage { get; set; }
        public TagNode pv7_current { get; set; }
        public TagNode pv8_voltage { get; set; }
        public TagNode pv8_current { get; set; }
        public TagNode pv9_voltage { get; set; }
        public TagNode pv9_current { get; set; }
        public TagNode pv10_voltage { get; set; }
        public TagNode pv10_current { get; set; }
        public TagNode pv11_voltage { get; set; }
        public TagNode pv11_current { get; set; }
        public TagNode pv12_voltage { get; set; }
        public TagNode pv12_current { get; set; }
        public TagNode pv13_voltage { get; set; }
        public TagNode pv13_current { get; set; }
        public TagNode pv14_voltage { get; set; }
        public TagNode pv14_current { get; set; }
        public TagNode pv15_voltage { get; set; }
        public TagNode pv15_current { get; set; }
        public TagNode pv16_voltage { get; set; }
        public TagNode pv16_current { get; set; }
        public TagNode pv17_voltage { get; set; }
        public TagNode pv17_current { get; set; }
        public TagNode pv18_voltage { get; set; }
        public TagNode pv18_current { get; set; }
        public TagNode pv19_voltage { get; set; }
        public TagNode pv19_current { get; set; }
        public TagNode pv20_voltage { get; set; }
        public TagNode pv20_current { get; set; }
        public TagNode mppt1_voltage { get; set; }
        public TagNode mppt1_current { get; set; }
        public TagNode mppt1_power { get; set; }
        public TagNode mppt2_voltage { get; set; }
        public TagNode mppt2_current { get; set; }
        public TagNode mppt2_power { get; set; }
        public TagNode mppt3_voltage { get; set; }
        public TagNode mppt3_current { get; set; }
        public TagNode mppt3_power { get; set; }
        public TagNode mppt4_voltage { get; set; }
        public TagNode mppt4_current { get; set; }
        public TagNode mppt4_power { get; set; }
        public TagNode mppt5_voltage { get; set; }
        public TagNode mppt5_current { get; set; }
        public TagNode mppt5_power { get; set; }
        public TagNode mppt6_voltage { get; set; }
        public TagNode mppt6_current { get; set; }
        public TagNode mppt6_power { get; set; }
        public TagNode mppt7_voltage { get; set; }
        public TagNode mppt7_current { get; set; }
        public TagNode mppt7_power { get; set; }
        public TagNode mppt8_voltage { get; set; }
        public TagNode mppt8_current { get; set; }
        public TagNode mppt8_power { get; set; }
        public TagNode mppt9_voltage { get; set; }
        public TagNode mppt9_current { get; set; }
        public TagNode mppt9_power { get; set; }
        public TagNode mppt10_voltage { get; set; }
        public TagNode mppt10_current { get; set; }
        public TagNode mppt10_power { get; set; }

        [Subscribe]
        public TagNode mccb { get; set; }
        [Subscribe]
        public TagNode trip { get; set; }

        public List<TagNode> MPPT_Voltage_Tags { get; set; } = new List<TagNode>();
        public List<TagNode> MPPT_Current_Tags { get; set; } = new List<TagNode>();
        public List<TagNode> MPPT_Power_Tags { get; set; } = new List<TagNode>();

        private void GetPVTags()
        {
            MPPT_Voltage_Tags.Clear();
            MPPT_Current_Tags.Clear();
            MPPT_Power_Tags.Clear();

            var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            for (int i = 1; i <= StringCount; i++)
            {
                TagNode voltageTag = properties.FirstOrDefault(x => x.Name.StartsWith($"mppt{i}_voltage")).GetValue(this) as TagNode;
                TagNode currentTag = properties.FirstOrDefault(x => x.Name.StartsWith($"mppt{i}_current")).GetValue(this) as TagNode;
                TagNode powerTag = properties.FirstOrDefault(x => x.Name.StartsWith($"mppt{i}_power")).GetValue(this) as TagNode;

                MPPT_Voltage_Tags.Add(voltageTag);
                MPPT_Current_Tags.Add(currentTag);
                MPPT_Power_Tags.Add(powerTag);
            }
        }

        private void Device_status_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (device_status != null)
            {
                if (device_status.Quality == Quality.Good)
                {
                    if (int.TryParse(device_status.Value, out int statusCode))
                    {
                        string status = "Offline";
                        switch (statusCode)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 0xA00:
                                status = "Standby";
                                break;
                            case 0x100:
                                status = "Starting";
                                break;
                            case 0x200:
                                status = "On-grid";
                                break;
                            case 0x201:
                                status = "Power limited";
                                break;
                            case 0x202:
                                status = "Self Derating";
                                break;
                            case 0x300:
                                status = "Shutdown Fault";
                                break;
                            case 0x301:
                            case 0x302:
                            case 0x303:
                            case 0x304:
                            case 0x305:
                            case 0x306:
                            case 0x307:
                            case 0x308:
                                status = "Shutdown";
                                break;
                            default:
                                status = "Unknown";
                                break;
                        }

                        Status = status;
                    }
                    else
                    {
                        Status = "Offline";
                    }
                    return;
                }
            }

            Status = "Offline";
        }

        private void Device_status_QualityChanged(object sender, TagQualityChangedEventArgs e)
        {
            Device_status_ValueChanged(null, null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
