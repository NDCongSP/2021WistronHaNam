using AFSClient;
using ipc_hanam.models;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
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
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using System.Windows.Threading;

namespace ipc_hanam
{
    /// <summary>
    /// Interaction logic for InverterPowerTrendView.xaml
    /// </summary>
    public partial class InverterPowerTrendView : UserControl
    {
        private System.Timers.Timer _refreshTime;
        CartesianMapper<DateTimePoint> config;
        private LineSeries powerLine;
        private LineSeries radiantLine;

        public InverterModel Inverter { get; set; }
        public InverterPowerTrendView()
        {
            InitializeComponent();
            ConfigTrend();
        }

        public void LoadInverter(InverterModel inverter)
        {
            Inverter = inverter;
            DataContext = this;
            if (_refreshTime == null)
            {
                _refreshTime = new System.Timers.Timer();
                _refreshTime.Interval = TimeSpan.FromMinutes(5).TotalMilliseconds;
                _refreshTime.Elapsed += _refreshTime_Elapsed;
                _refreshTime_Elapsed(null, null);
            }
        }

        private void ConfigTrend()
        {
            // Config trend
            trend.Zoom = ZoomingOptions.None;
            trend.ScrollMode = ScrollMode.None;

            yPower.MaxValue = 1000;
            yPower.MinValue = -1000;
            yRadiant.MaxValue = 1000;
            yRadiant.MinValue = -1000;

            xTime.LabelFormatter = (x) =>
            {
                if (x <= 0 || double.IsNaN(x) || double.IsInfinity(x))
                    return "";

                return new DateTime((long)(x)).ToString("HH:mm");
            };
            xTime.Separator.Step = TimeSpan.FromHours(1).Ticks;

            powerLine = new LineSeries()
            {
                PointGeometry = DefaultGeometries.None,
                Title = "Power (kW)",
                ScalesYAt = 0,
                FontSize = 16,
                Visibility = Visibility.Visible,
                Stroke = new SolidColorBrush(Colors.Red),
                Values = new ChartValues<DateTimePoint>(),
                Fill = Brushes.Transparent,
            };

            radiantLine = new LineSeries()
            {
                PointGeometry = DefaultGeometries.None,
                Title = "Radiant (W/m2)",
                FontSize = 16,
                ScalesYAt = 1,
                Stroke = new SolidColorBrush(System.Windows.Media.Colors.Green),
                Values = new ChartValues<DateTimePoint>(),
                Fill = Brushes.Transparent,
            };

            config = new CartesianMapper<DateTimePoint>().X(x =>
            {
                return TimeSpan.FromHours(x.DateTime.Hour).Add(TimeSpan.FromMinutes(x.DateTime.Minute)).Ticks;
            }).Y(x =>
            {
                return x.Value;
            });

            trend.Series = new SeriesCollection(config);
            trend.Series.Add(powerLine);
            trend.Series.Add(radiantLine);
            trend.MouseMove += OnTrendMouseMove;

            trend.DataTooltip.Background = new SolidColorBrush(Color.FromArgb(120, 128, 128, 128));
        }

        private void _refreshTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _refreshTime.Stop();
            try
            {
                RefreshTrend();
            }
            catch { }
            _refreshTime.Start();
        }

        private void OnTrendMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            //lets get where the mouse is at our chart
            var mouseCoordinate = e.GetPosition(trend);

            //now that we know where the mouse is, lets use
            //ConverToChartValues extension
            //it takes a point in pixes and scales it to our chart current scale/values
            var p1 = trend.ConvertToChartValues(mouseCoordinate);
            var p2 = trend.ConvertToChartValues(mouseCoordinate, 0, 1);

            sectionY1.Value = Math.Truncate(p1.Y);
            sectionY2.Value = Math.Truncate(p2.Y);

            var series = trend.Series[0];
            var closetsPoint = series.ClosestPointTo(p1.X, AxisOrientation.X);

            if (closetsPoint != null)
                sectionTime.Value = closetsPoint.X;
            else
                sectionTime.Value = double.NaN;
        }

        public async void RefreshTrend()
        {
            try
            {
                powerLine.Values.Clear();
                radiantLine.Values.Clear();

                IServerConnector connector = ServerConnectorProvider.GetConnector(0);
                List<InverterLogRecord> items = null;

                DateTime fromTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime toTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                if (connector != null)
                {
                    var result = await connector.GetHistoryItemsAsync<InverterLogRecord>(Inverter.HistoryPath, fromTime, toTime);
                    items = result?.ToList();

                }

                Dispatcher.Invoke(() =>
                {
                    try
                    {
                        xTime.MinValue = 0;
                        xTime.MaxValue = TimeSpan.FromDays(1).Ticks;

                        if (Inverter != null && items != null)
                        {
                            List<DateTimePoint> powerPoints = new List<DateTimePoint>();
                            List<DateTimePoint> radiantPoints = new List<DateTimePoint>();
                            double maxY = 100;
                            double minY = 0;

                            foreach (var item in items)
                            {
                                var time = new DateTime(item.DateTime.Year, item.DateTime.Month, item.DateTime.Day, item.DateTime.Hour, item.DateTime.Minute, 0);
                                powerPoints.Add(new DateTimePoint()
                                {
                                    DateTime = time,
                                    Value = item.ActivePower == null ? 0 : item.ActivePower.Value
                                });
                                radiantPoints.Add(new DateTimePoint()
                                {
                                    DateTime = time,
                                    Value = item.Radiation == null ? 0 : item.Radiation.Value
                                });

                                if (item.ActivePower != null)
                                {
                                    maxY = Math.Max(maxY, item.ActivePower.Value + 100);
                                    minY = Math.Min(minY, item.ActivePower.Value);
                                }


                                if (item.Radiation != null)
                                {
                                    maxY = Math.Max(maxY, item.Radiation.Value + 100);
                                    minY = Math.Min(minY, item.Radiation.Value);
                                }
                            }

                            foreach (var y in trend.AxisY)
                            {
                                y.MaxValue = maxY;
                                y.MinValue = minY;
                            };

                            powerLine.Values.AddRange(powerPoints);
                            radiantLine.Values.AddRange(radiantPoints);

                            trend.Update();
                        }
                    }
                    catch { }
                });
            }
            catch { }
        }
    }
}
