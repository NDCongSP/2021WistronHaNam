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
    /// Interaction logic for ZeroExportPowerMeterOverviewView.xaml
    /// </summary>
    public partial class ZeroExportPowerMeterOverviewView : UserControl
    {
        public LoggerModel Logger { get; set; }
        public ZeroExportPowerMeterModel PowerMeter { get; set; }
        public ZeroExportPowerMeterPage PowerMeterPage { get; set; }

        public ZeroExportPowerMeterOverviewView(LoggerModel logger, ZeroExportPowerMeterModel powerMeter)
        {
            InitializeComponent();
            Logger = logger;
            PowerMeter = powerMeter;
            PowerMeterPage = new ZeroExportPowerMeterPage(logger, powerMeter);
            DataContext = this;
        }
    }
}
