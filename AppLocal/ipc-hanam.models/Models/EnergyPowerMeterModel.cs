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
    public class EnergyPowerMeterModel : INotifyPropertyChanged
    {
        string _tagPrefix;
        string _status = "Offline";

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
                if (_tagPrefix != value)
                {
                    _tagPrefix = value;
                    RaisePropertyChanged();
                    this.LoadTags(true);
                    if (voltage_ab != null)
                    {
                        voltage_ab.QualityChanged += Voltage_ab_QualityChanged;
                    }
                    Voltage_ab_QualityChanged(null, null);
                }
            }
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
        public MeterType MeterType { get; set; } = MeterType.Energy;

        [DisplayName("Phase AB Voltage")]
        public TagNode voltage_ab { get; set; }

        [DisplayName("Phase BC Voltage")]
        public TagNode voltage_bc { get; set; }

        [DisplayName("Phase CA Voltage")]
        public TagNode voltage_ca { get; set; }

        [DisplayName("Phase LL Avg Voltage")]
        public TagNode voltage_ll_avg { get; set; }

        [DisplayName("Phase AN Voltage")]
        public TagNode voltage_an { get; set; }

        [DisplayName("Phase BN Voltage")]
        public TagNode voltage_bn { get; set; }

        [DisplayName("Phase CN Voltage")]
        public TagNode voltage_cn { get; set; }

        [DisplayName("Phase LN Avg Voltage")]
        public TagNode voltage_ln_avg { get; set; }

        [DisplayName("Phase A Current")]
        public TagNode current_a { get; set; }

        [DisplayName("Phase B Current")]
        public TagNode current_b { get; set; }

        [DisplayName("Phase C Current")]
        public TagNode current_c { get; set; }

        [DisplayName("Phase N Current")]
        public TagNode current_n { get; set; }

        [DisplayName("Phase Avg Current")]
        public TagNode current_avg { get; set; }

        [DisplayName("Phase A Active Power")]
        public TagNode active_power_a { get; set; }

        [DisplayName("Phase B Active Power")]
        public TagNode active_power_b { get; set; }

        [DisplayName("Phase C Active Power")]
        public TagNode active_power_c { get; set; }

        [DisplayName("Total Active Power")]
        public TagNode active_power_total { get; set; }

        [DisplayName("Phase A Reactive Power")]
        public TagNode reactive_power_a { get; set; }

        [DisplayName("Phase B Reactive Power")]
        public TagNode reactive_power_b { get; set; }

        [DisplayName("Phase C Reactive Power")]
        public TagNode reactive_power_c { get; set; }

        [DisplayName("Total Reactive Power")]
        public TagNode reactive_power_total { get; set; }

        [DisplayName("Phase A Power Factor")]
        public TagNode power_factor_a { get; set; }

        [DisplayName("Phase B Power Factor")]
        public TagNode power_factor_b { get; set; }

        [DisplayName("Phase C Power Factor")]
        public TagNode power_factor_c { get; set; }

        [DisplayName("Total Power Factor")]
        public TagNode power_factor_total { get; set; }

        [DisplayName("Frequency")]
        public TagNode frequency { get; set; }

        [DisplayName("Total Energy Delivered")]
        public TagNode active_energy_delivered { get; set; }

        [DisplayName("THD Voltage AB")]
        public TagNode thd_voltage_ab { get; set; }

        [DisplayName("THD Voltage BC")]
        public TagNode thd_voltage_bc { get; set; }

        [DisplayName("THD Voltage CA")]
        public TagNode thd_voltage_ca { get; set; }

        [DisplayName("THD Voltage LL")]
        public TagNode thd_voltage_ll { get; set; }

        [DisplayName("THD Voltage AN")]
        public TagNode thd_voltage_an { get; set; }

        [DisplayName("THD Voltage BN")]
        public TagNode thd_voltage_bn { get; set; }

        [DisplayName("THD Voltage CN")]
        public TagNode thd_voltage_cn { get; set; }

        [DisplayName("THD Voltage LN")]
        public TagNode thd_voltage_ln { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Voltage_ab_QualityChanged(object sender, TagQualityChangedEventArgs e)
        {
            if (voltage_ab != null)
            {
                if (voltage_ab.Quality == Quality.Good)
                {
                    Status = "Online";
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
