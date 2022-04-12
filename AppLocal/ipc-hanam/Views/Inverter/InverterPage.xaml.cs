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
    /// Interaction logic for InverterPage.xaml
    /// </summary>
    public partial class InverterPage : UserControl, IHaveTitle
    {
        public InverterModel Inverter { get; set; }
        public LoggerModel Logger { get; set; }
        public string Title { get; }
        public Uri IconSource { get; }

        public InverterPage(LoggerModel logger, InverterModel inverter)
        {
            InitializeComponent();
            Logger = logger;
            Inverter = inverter;
            Title = $"{logger.DisplayName} / {inverter.DisplayName}";

            _basicInfo.LoadInverter(inverter);
            _energyYields.LoadInverter(inverter);
            _acOutput.LoadInverter(inverter);
            _dcOutput.LoadInverter(inverter);
            _invPowerTrend.LoadInverter(inverter);
            DataContext = this;
            _wrapPanel.TagPrefix = inverter.TagPrefix;
        }
    }
}
