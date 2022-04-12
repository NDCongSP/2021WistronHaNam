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
    /// Interaction logic for EnviromentBenefitsView.xaml
    /// </summary>
    public partial class EnviromentBenefitsView : UserControl
    {
        public IoC IoC { get; set; }
        
        public EnviromentBenefitsView()
        {
            InitializeComponent();
            IoC = IoC.Instance;
            DataContext = this;
        }
    }
}
