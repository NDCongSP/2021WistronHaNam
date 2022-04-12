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
    /// Interaction logic for MccbView.xaml
    /// </summary>
    public partial class MccbView : UserControl
    {
        public InverterModel Inverter { get; set; }
        public InverterPage InverterPage { get; set; }

        public MccbView(InverterModel inverter, InverterPage inverterPage, int index)
        {
            InitializeComponent();
            Inverter = inverter;
            Inverter.PropertyChanged += Inverter_PropertyChanged;
            _title.Content = $"#{index}";
            InverterPage = inverterPage;
            _inv.PreviewMouseLeftButtonDown += _inv_PreviewMouseLeftButtonDown;
            DataContext = this;
            Inverter_PropertyChanged(null, null);
        }

        private void Inverter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e != null)
                {
                    if (e.PropertyName == nameof(InverterModel.MCCBStatus))
                    {
                        _lbMccbStatus.Content = Inverter.MCCBStatus;
                        switch (Inverter.MCCBStatus)
                        {
                            case "Opened":
                                _rectMccb.Fill = Brushes.Green;
                                _rotate.Angle = -30;
                                break;
                            case "Closed":
                                _rectMccb.Fill = Brushes.Red;
                                _rotate.Angle = 0;
                                break;
                            case "Trip":
                                _rectMccb.Fill = Brushes.Orange;
                                _rotate.Angle = -30;
                                break;
                            default:
                                _rectMccb.Fill = Brushes.Transparent;
                                break;
                        }
                    }
                }
                else
                {
                    _lbMccbStatus.Content = Inverter.MCCBStatus;
                    switch (Inverter.MCCBStatus)
                    {
                        case "Opened":
                            _rectMccb.Fill = Brushes.Green;
                            _rotate.Angle = -30;
                            break;
                        case "Closed":
                            _rectMccb.Fill = Brushes.Red;
                            _rotate.Angle = 0;
                            break;
                        case "Trip":
                            _rectMccb.Fill = Brushes.Orange;
                            _rotate.Angle = -30;
                            break;
                        default:
                            _rectMccb.Fill = Brushes.Transparent;
                            break;
                    }
                }
            });
        }

        private void _inv_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainPage.Instance.NavigateTo(InverterPage);
        }
    }
}
