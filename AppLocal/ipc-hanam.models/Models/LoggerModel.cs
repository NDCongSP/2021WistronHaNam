using AFSClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public class LoggerModel : INotifyPropertyChanged
    {
        string _tagPrefix;
        string _status = "Offline";
        string _acbStatus = "Offline";
        int _inverterRunnings = 0;

        public string DisplayName { get; set; }
        public int Index { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string DeviceId { get; set; }
        public string TagPrefix
        {
            get => _tagPrefix;
            set
            {
                _tagPrefix = value;
                this.LoadTags();
                if (plant_status != null)
                {
                    plant_status.ValueChanged += Plant_status_ValueChanged;
                    plant_status.QualityChanged += Plant_status_QualityChanged;
                }
                Plant_status_ValueChanged(null, null);

                if (acb != null)
                {
                    acb.ValueChanged += Acb_ValueChanged;
                    acb.QualityChanged += Acb_QualityChanged;
                }

                if (acb_trip != null)
                {
                    acb_trip.ValueChanged += Acb_ValueChanged;
                    acb_trip.QualityChanged += Acb_QualityChanged;
                }
            }
        }

        private void Acb_QualityChanged(object sender, TagQualityChangedEventArgs e)
        {
            Acb_ValueChanged(null, null);
        }

        private void Acb_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (acb != null && acb_trip != null)
            {
                if (acb.Quality == Quality.Good &&
                    acb_trip.Quality == Quality.Good)
                {
                    if (acb_trip.Value == "1")
                    {
                        ACBStatus = "Trip";
                    }
                    else if (acb_trip.Value == "0")
                    {
                        if (acb.Value == "1")
                        {
                            ACBStatus = "Closed";
                        }
                        else if (acb.Value == "0")
                        {
                            ACBStatus = "Opened";
                        }
                        else
                        {
                            ACBStatus = "Offline";
                        }
                    }
                    else
                    {
                        ACBStatus = "Offline";
                    }
                    return;
                }
            }
            ACBStatus = "Offline";
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
                }
            }
        }
        public string ACBStatus
        {
            get => _acbStatus;
            set
            {
                if (_acbStatus != value)
                {
                    _acbStatus = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int InverterRunnings
        {
            get => _inverterRunnings;
            set
            {
                if (_inverterRunnings != value)
                {
                    _inverterRunnings = value;
                    RaisePropertyChanged();
                }
            }
        }
        public List<InverterModel> Inverters { get; set; } = new List<InverterModel>();

        public EnergyPowerMeterModel EnergyPowerMeter { get; set; }
        public ElectricalQualityMeterModel ElectricalQualityMeter { get; set; }
        public ZeroExportPowerMeterModel ZeroExportPowerMeter { get; set; }

        public void Start()
        {
            foreach (var inv in Inverters)
            {
                inv.PropertyChanged += Inv_PropertyChanged;
                Inv_PropertyChanged(null, null);
            }
        }

        private void Inv_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e == null || e.PropertyName == nameof(InverterModel.IsRunning))
            {
                InverterRunnings = Inverters.Count(x => x.IsRunning);
            }
        }

        // Tags
        public TagNode dc_current { get; set; }
        public TagNode input_power { get; set; }
        public TagNode co2_reduction { get; set; }
        public TagNode active_power { get; set; }
        public TagNode reactive_power { get; set; }
        [Subscribe]
        public TagNode plant_status { get; set; }
        public TagNode total_energy_yields { get; set; }
        public TagNode daily_energy_yields { get; set; }
        public TagNode phase_a_current { get; set; }
        public TagNode phase_b_current { get; set; }
        public TagNode phase_c_current { get; set; }
        public TagNode phase_ab_voltage { get; set; }
        public TagNode phase_bc_voltage { get; set; }
        public TagNode phase_ca_voltage { get; set; }
        [Subscribe]
        public TagNode acb { get; set; }
        [Subscribe]
        public TagNode acb_trip { get; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Plant_status_QualityChanged(object sender, TagQualityChangedEventArgs e)
        {
            Plant_status_ValueChanged(null, null); 
        }

        private void Plant_status_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (plant_status != null)
            {
                if (plant_status.Quality == Quality.Good)
                {
                    switch (plant_status.Value)
                    {
                        case "1":
                            Status = "Power Operation";
                            break;
                        case "2":
                            Status = "Idle";
                            break;
                        case "3":
                            Status = "Outage";
                            break;
                        case "4":
                            Status = "Communication Interrupt";
                            break;
                        default:
                            Status = "Undefined";
                            break;
                    }
                }
                else
                {
                    Status = "Offline";
                }
            }
            else
            {
                Status = "Offline";
            }
        }
    }
}
