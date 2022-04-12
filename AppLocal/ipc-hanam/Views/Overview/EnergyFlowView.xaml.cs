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
    /// Interaction logic for EnergyFlowView.xaml
    /// </summary>
    public partial class EnergyFlowView : UserControl
    {
        public IoC IoC { get; set; }

        public EnergyFlowView()
        {
            InitializeComponent();
            IoC = IoC.Instance;
            DataContext = this;
        }
    }
}
