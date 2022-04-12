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
    /// Interaction logic for BasicInformationView.xaml
    /// </summary>
    public partial class BasicInformationView : UserControl
    {
        public StationModel Station { get; set; }

        public BasicInformationView()
        {
            InitializeComponent();
            Station = IoC.Instance.Station;
            DataContext = this;
        }
    }
}
