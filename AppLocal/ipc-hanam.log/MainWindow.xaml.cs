using AFSClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ipc_hanam.log
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IServerConnector ServerConnector { get; set; }

        public TagNode wind_speed { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ServerConnector = ServerConnectorProvider.GetConnector(0);
            ServerConnector_Started(null, null);
            ServerConnector.Start();
        }

        private async void ServerConnector_Started(object sender, EventArgs e)
        {
            wind_speed = await ServerConnectorProvider.SubscribeTag("0/Connectivity/summary/tags/wind_speed");
            wind_speed.ValueChanged += Wind_speed_ValueChanged;
        }

        private void Wind_speed_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            Debug.WriteLine(wind_speed.Value);
        }
    }
}
