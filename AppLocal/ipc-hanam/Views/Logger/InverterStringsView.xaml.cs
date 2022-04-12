using AFSClient;
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
using static System.Collections.Specialized.BitVector32;

namespace ipc_hanam
{
/// <summary>
/// Interaction logic for InverterStringsView.xaml
/// </summary>
    public partial class InverterStringsView : UserControl
    {
        private System.Timers.Timer _refreshTimer;
        private LoggerModel _logger;
        private Dictionary<InverterModel, List<Label>> _labelDic = new Dictionary<InverterModel, List<Label>>();

        public InverterStringsView()
        {
            InitializeComponent();
            _labelDic = new Dictionary<InverterModel, List<Label>>();
        }

        public void LoadLogger(LoggerModel logger)
        {
            _logger = logger;
            _labelDic.Clear();
            stackParent.Children.Clear();

            if (_logger != null)
            {
                _lbTitle.Content = logger.DisplayName;

                foreach (var inv in _logger.Inverters)
                {
                    _labelDic[inv] = new List<Label>();

                    StackPanel inverterStack = new StackPanel()
                    {
                        Orientation = Orientation.Vertical,
                        Tag = inv,
                        Margin = new Thickness(10, 0, 10, 0)
                    };

                    for (int i = 0; i <= inv.StringCount; i++)
                    {
                        Rectangle rect = new Rectangle()
                        {
                            Fill = Brushes.Orange,
                            Width = 2,
                            Height = 10,
                        };
                        inverterStack.Children.Add(rect);
                        if (i == 0)
                        {
                            Label label = new Label()
                            {
                                Content = inv.DisplayName,
                                BorderThickness = new Thickness(1),
                                BorderBrush = Brushes.Orange,
                                Background = Brushes.AliceBlue,
                                Foreground = Brushes.Black,
                                Width = 80,
                                HorizontalContentAlignment = HorizontalAlignment.Center
                            };
                            inverterStack.Children.Add(label);
                        }
                        else
                        {
                            Label label = new Label()
                            {
                                Content = "0 W",
                                BorderThickness = new Thickness(1),
                                BorderBrush = Brushes.Orange,
                                Background = Brushes.Red,
                                Foreground = Brushes.Black,
                                Width = 80,
                                HorizontalContentAlignment = HorizontalAlignment.Center,
                                Tag = inv,
                            };
                            inverterStack.Children.Add(label);
                            _labelDic[inv].Add(label);
                        }
                    }

                    stackParent.Children.Add(inverterStack);
                }

            }

            if (_refreshTimer == null)
            {
                _refreshTimer = new System.Timers.Timer();
                _refreshTimer.Interval = 1000;
                _refreshTimer.Elapsed += _refreshTimer_Elapsed;
                _refreshTimer.Start();
            }
            Refresh();
        }

        private void _refreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _refreshTimer.Start();
            try
            {
                Refresh();
            }
            catch { }
            _refreshTimer.Start();
        }

        public void Refresh()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    if (_labelDic != null)
                    {
                        foreach (var kvp in _labelDic)
                        {
                            InverterModel inv = kvp.Key;

                            for (int i = 0; i < kvp.Value.Count; i++)
                            {
                                TagNode mppt_voltage = inv.MPPT_Voltage_Tags[i];
                                TagNode mppt_current = inv.MPPT_Current_Tags[i];

                                Label currentLabel = kvp.Value[i];

                                SolidColorBrush background = Brushes.Red;
                                if (mppt_voltage != null && mppt_voltage.Quality == Quality.Good &&
                                    mppt_current != null && mppt_current.Quality == Quality.Good)
                                {
                                    background = Brushes.Green;
                                }

                                double power = 0;
                                if (mppt_current != null && double.TryParse(mppt_current.Value, out double current) &&
                                    mppt_voltage != null && double.TryParse(mppt_voltage.Value, out double voltage))
                                {
                                    power = current * voltage / 1000;
                                    power = Math.Round(power, 1);
                                }

                                if (power <= 0)
                                {
                                    background = Brushes.Red;
                                }

                                currentLabel.Background = background;
                                currentLabel.Content = $"{power} kW";
                            }
                        }
                    }
                }
                catch { }
            });
        }
    }
}
