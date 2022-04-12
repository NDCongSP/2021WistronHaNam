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
    /// Interaction logic for LoggerOverviewView.xaml
    /// </summary>
    public partial class LoggerOverviewView : UserControl
    {
        public LoggerModel Logger { get; set; }
        public LoggerPage LoggerPage { get; set; }

        public LoggerOverviewView(LoggerModel logger)
        {
            InitializeComponent();
            Logger = logger;
            LoggerPage = new LoggerPage(logger);
            _wrapPanel.TagPrefix = logger.TagPrefix;
            DataContext = this;
        }
    }
}
