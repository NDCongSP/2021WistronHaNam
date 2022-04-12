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
    /// Interaction logic for ElectricalQualityMeterOverviewView.xaml
    /// </summary>
    public partial class ElectricalQualityMeterOverviewView : UserControl
    {
        public LoggerModel Logger { get; set; }
        public ElectricalQualityMeterModel PowerMeter { get; set; }
        public ElectricalQualityMeterPage ElectricalQualityMeterPage { get; set; }

        public ElectricalQualityMeterOverviewView(LoggerModel logger, ElectricalQualityMeterModel powerMeter)
        {
            InitializeComponent();
            Logger = logger;
            PowerMeter = powerMeter;
            ElectricalQualityMeterPage = new ElectricalQualityMeterPage(Logger, powerMeter);
            DataContext = this;
        }
    }
}
