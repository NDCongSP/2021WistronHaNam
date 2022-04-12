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
    /// Interaction logic for InverterEnergyYieldsView.xaml
    /// </summary>
    public partial class InverterEnergyYieldsView : UserControl
    {
        public InverterModel Inverter { get; set; }

        public InverterEnergyYieldsView()
        {
            InitializeComponent();
        }

        public void LoadInverter(InverterModel inverter)
        {
            Inverter = inverter;
            DataContext = this;
        }
    }
}
