using ipc_hanam.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for InverterACOutputView.xaml
    /// </summary>
    public partial class InverterACOutputView : UserControl, INotifyPropertyChanged
    {
        public InverterModel Inverter { get; set; }

        public InverterACOutputView()
        {
            InitializeComponent();
            _lbActivePower.InheritPathChanged += _lbActivePower_InheritPathChanged;
        }

        private void _lbActivePower_InheritPathChanged(object sender, EventArgs e)
        {
            string path = _lbActivePower.InheritPath;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadInverter(InverterModel inverter)
        {
            Inverter = inverter;
            DataContext = this;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Inverter"));
        }
    }
}
