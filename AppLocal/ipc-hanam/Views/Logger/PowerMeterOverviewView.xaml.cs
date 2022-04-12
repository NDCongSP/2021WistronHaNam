using ipc_hanam.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ipc_hanam
{
    /// <summary>
    /// Interaction logic for PowerMeterOververView.xaml
    /// </summary>
    public partial class PowerMeterOverviewView : UserControl
    {
        public LoggerModel Logger { get; set; }
        public EnergyPowerMeterModel PowerMeter { get; set; }
        public PowerMeterPage PowerMeterPage { get; set; }

        public PowerMeterOverviewView(LoggerModel logger, EnergyPowerMeterModel powerMeter)
        {
            InitializeComponent();
            Logger = logger;
            PowerMeter = powerMeter;
            PowerMeterPage = new PowerMeterPage(logger, powerMeter);
            DataContext = this;
        }
    }
}
