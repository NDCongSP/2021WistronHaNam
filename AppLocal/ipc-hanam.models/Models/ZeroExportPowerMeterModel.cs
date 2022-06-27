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
    public class ZeroExportPowerMeterModel : INotifyPropertyChanged
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
        public MeterType MeterType { get; set; } = MeterType.ZeroExport;

        public TagNode voltage_an { get; set; }
        public TagNode voltage_bn { get; set; }
        public TagNode voltage_cn { get; set; }
        public TagNode voltage_ab { get; set; }
        public TagNode voltage_bc { get; set; }
        public TagNode voltage_ca { get; set; }
        public TagNode current_a { get; set; }
        public TagNode current_b { get; set; }
        public TagNode current_c { get; set; }
        [Subscribe]
        public TagNode active_power_total { get; set; }
        public TagNode reactive_power_total { get; set; }
        public TagNode power_factor { get; set; }
        public TagNode active_power_a { get; set; }
        public TagNode active_power_b { get; set; }
        public TagNode active_power_c { get; set; }
        [Subscribe]
        public TagNode active_energy_delivered { get; set; }


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
