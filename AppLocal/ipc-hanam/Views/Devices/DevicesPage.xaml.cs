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
    /// Interaction logic for DevicesPage.xaml
    /// </summary>
    public partial class DevicesPage : UserControl
    {
        public List<LoggerOverviewView> LoggerOverviewViews { get; set; }

        public DevicesPage()
        {
            InitializeComponent();
            LoggerOverviewViews = new List<LoggerOverviewView>();
            foreach (var logger in IoC.Instance.Station.Loggers)
            {
                var view = CreateLoggerOverviewView(logger);
                view.Width = 420;
                view.Margin = new Thickness(16);
                LoggerOverviewViews.Add(view);
                _wrapPanel.Children.Add(view);
            }
        }

        private LoggerOverviewView CreateLoggerOverviewView(LoggerModel loggerModel)
        {
            LoggerOverviewView view = new LoggerOverviewView(loggerModel);
            return view;
        }
    }
}
