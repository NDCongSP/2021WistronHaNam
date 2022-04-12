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
    /// Interaction logic for InverterOverviewView.xaml
    /// </summary>
    public partial class InverterOverviewView : UserControl
    {
        public InverterModel Inverter { get; set; }
        public LoggerModel Logger { get; set; }
        public InverterPage InverterPage { get; set; }

        public InverterOverviewView(LoggerModel logger, InverterModel inverter)
        {
            InitializeComponent();
            Logger = logger;
            Inverter = inverter;
            InverterPage = new InverterPage(logger, inverter);
            _panel.TagPrefix = inverter.TagPrefix;
            DataContext = this;
        }
    }
}
