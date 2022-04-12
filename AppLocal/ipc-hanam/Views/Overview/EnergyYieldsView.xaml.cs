using AFSClient;
using AFSClient.QueryBuilder;
using ipc_hanam.models;
using LiveCharts.Defaults;
using LiveCharts;
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
using static System.Collections.Specialized.BitVector32;

namespace ipc_hanam
{
    /// <summary>
    /// Interaction logic for EnergyYieldsView.xaml
    /// </summary>
    public partial class EnergyYieldsView : UserControl
    {
        public IoC IoC { get; set; }

        public EnergyYieldsView()
        {
            InitializeComponent();
            IoC = IoC.Instance;
            DataContext = this;
        }

    }

}
