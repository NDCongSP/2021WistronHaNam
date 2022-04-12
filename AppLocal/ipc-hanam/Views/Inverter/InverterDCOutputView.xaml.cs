using AFS.HMILibrary.Wpf;
using AFSClient;
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
    /// Interaction logic for InverterDCOutputView.xaml
    /// </summary>
    public partial class InverterDCOutputView : UserControl
    {
        public InverterModel Inverter { get; set; }

        public InverterDCOutputView()
        {
            InitializeComponent();
        }

        public void LoadInverter(InverterModel inverter)
        {
            Inverter = inverter;
            DataContext = this;

            for (int i = 0; i < inverter.MPPT_Voltage_Tags.Count; i++)
            {
                TagNode voltageTag = inverter.MPPT_Voltage_Tags[i];
                TagNode currentTag = inverter.MPPT_Current_Tags[i];

                TagValueDisplayView voltageView = new TagValueDisplayView(voltageTag, $"Mppt {i + 1} Voltage");
                TagValueDisplayView currentView = new TagValueDisplayView(currentTag, $"Mppt {i + 1} Current");


                _voltageStack.Children.Add(voltageView);
                _currentStack.Children.Add(currentView);
            }
        }
    }
}
