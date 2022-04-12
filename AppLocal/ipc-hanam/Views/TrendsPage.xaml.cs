using AFSClient;
using ipc_hanam.models;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Microsoft.Win32;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.IO;
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
using Query = SqlKata.Query;

namespace ipc_hanam
{
    /// <summary>
    /// Interaction logic for TrendsPage.xaml
    /// </summary>
    public partial class TrendsPage : UserControl
    {
        private LineSeries powerLine;
        private LineSeries radiantLine;
        private LineSeries temperatureLine;
        // private LineSeries energyLine;
        CartesianMapper<DateTimePoint> config;

        public ChartValues<ObservablePoint> DayTrendValues { get; set; } = new ChartValues<ObservablePoint>();
        public ChartValues<ObservablePoint> MonthTrendValues { get; set; } = new ChartValues<ObservablePoint>();

        List<EnergyTrendDataSource> _energyTrendDataSources;
        EnergyTrendDataSource _allDataSource;

        public TrendsPage()
        {
            InitializeComponent();

            _energyTrendDataSources = new List<EnergyTrendDataSource>();
            _allDataSource = new EnergyTrendDataSource()
            {
                DisplayName = "All",
                TableName = "summary",
                EnergyColumnName = "Energy"
            };
            _energyTrendDataSources.Add(_allDataSource);

            int index = 1;
            foreach (var logger in IoC.Instance.Station.Loggers)
            {
                var ds = new EnergyTrendDataSource()
                {
                    DisplayName = logger.DisplayName,
                    TableName = $"pvs{index}_logger",
                    EnergyColumnName = "TotalEnergy"
                };
                _energyTrendDataSources.Add(ds);
                index++;
            }

            _cobDayEnergyDataSource.ItemsSource = _energyTrendDataSources;
            _cobDayEnergyDataSource.SelectedIndex = 0;
            _cobMonthEnergyDataSource.ItemsSource = _energyTrendDataSources;
            _cobMonthEnergyDataSource.SelectedIndex = 0;
            _dtpDayEnergy.SelectedDate = DateTime.Now;
            _dtpMonthEnergy.SelectedDate = DateTime.Now;

            _btnRefreshDayEnergy.Click += _btnRefreshDayEnergy_Click;
            _btnRefreshMonthEnergy.Click += _btnRefreshMonthEnergy_Click;
            _btnRefreshSensor.Click += _btnRefreshSensor_Click;
            _btnPrintDayEnergy.Click += _btnPrintDayEnergy_Click;
            _btnPrintMonthEnergy.Click += _btnPrintMonthEnergy_Click;
            _btnPrintSensor.Click += _btnPrintSensor_Click;

            _dtpFromSensor.SelectedDate = DateTime.Now;
            _dtpToSensor.SelectedDate = DateTime.Now;

            ConfigTrend();

            #region Init month chart
            _chartDayEnergy.DataTooltip.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            _chartDayEnergy.MouseMove += OnChartMouseMove;
            DateTime firstDay = DateTime.Now.GetFirstDayOfMonth();
            DateTime lastDay = DateTime.Now.GetLastDayOfMonth();
            xTimeDay.MaxValue = lastDay.Day + 1;
            xTimeDay.MinValue = 1;
            colSeriesDay.Values = DayTrendValues;
            for (int i = 1; i <= lastDay.Day; i++)
            {
                colSeriesDay.Values.Add(new ObservablePoint(i, 0));
            }
            #endregion

            #region Init year chart
            _chartMonthEnergy.DataTooltip.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            _chartMonthEnergy.MouseMove += OnChartMouseMove;
            colSeriesMonth.Values = MonthTrendValues;
            xTimeMonth.MaxValue = 13;
            xTimeMonth.MinValue = 1;
            for (int i = 1; i <= 12; i++)
            {
                colSeriesMonth.Values.Add(new ObservablePoint(i, 0));
            }
            #endregion
        }

        private void _btnPrintSensor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Report.csv";
                sfd.Filter = "Csv files (*.csv)|*.csv";
                if (sfd.ShowDialog() == true)
                {
                    if (_dgSensor.ItemsSource is List<SummaryLogRecord> source)
                    {
                        var csvString = CsvHelper.ToCsvString<SummaryLogRecord>(source);
                        File.WriteAllText(sfd.FileName, csvString);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void _btnRefreshSensor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dtpFromSensor.SelectedDate != null &&
                    _dtpToSensor.SelectedDate != null)
                {
                    powerLine.Values.Clear();
                    radiantLine.Values.Clear();
                    // energyLine.Values.Clear();
                    temperatureLine.Values.Clear();
                    _dgSensor.ItemsSource = null;

                    DateTime fromTime = new DateTime(
                        _dtpFromSensor.SelectedDate.Value.Year, 
                        _dtpFromSensor.SelectedDate.Value.Month,
                        _dtpFromSensor.SelectedDate.Value.Day, 0, 0, 0);

                    DateTime toTime = new DateTime(
                        _dtpToSensor.SelectedDate.Value.Year,
                        _dtpToSensor.SelectedDate.Value.Month,
                        _dtpToSensor.SelectedDate.Value.Day, 23, 59, 59);

                    IServerConnector connector = ServerConnectorProvider.GetConnector(0);
                    List<SummaryLogRecord> items = null;

                    if (fromTime >= toTime)
                    {
                        return;
                    }

                    if (connector != null)
                    {
                        var result = await connector.GetHistoryItemsAsync<SummaryLogRecord>("Historians/summary", fromTime, toTime);
                        items = result?.ToList();
                    }

                    Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            xTime.MinValue = fromTime.Ticks;
                            xTime.MaxValue = toTime.Ticks;

                            if (items != null)
                            {
                                _dgSensor.ItemsSource = items;

                                List<DateTimePoint> powerPoints = new List<DateTimePoint>();
                                List<DateTimePoint> radiantPoints = new List<DateTimePoint>();
                                List<DateTimePoint> tempPoints = new List<DateTimePoint>();
                                List<DateTimePoint> energyPoints = new List<DateTimePoint>();
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
                                    tempPoints.Add(new DateTimePoint()
                                    {
                                        DateTime = time,
                                        Value = item.Temperature == null ? 0 : item.Temperature.Value
                                    });
                                    energyPoints.Add(new DateTimePoint()
                                    {
                                        DateTime = time,
                                        Value = item.Energy == null ? 0 : item.Energy.Value
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

                                    //if (item.Energy != null)
                                    //{
                                    //    maxY = Math.Max(maxY, item.Energy.Value + 100);
                                    //    minY = Math.Min(minY, item.Energy.Value);
                                    //}


                                    if (item.Temperature != null)
                                    {
                                        maxY = Math.Max(maxY, item.Temperature.Value + 100);
                                        minY = Math.Min(minY, item.Temperature.Value);
                                    }
                                }

                                foreach (var y in trend.AxisY)
                                {
                                    y.MaxValue = maxY;
                                    y.MinValue = minY;
                                };

                                powerLine.Values.AddRange(powerPoints);
                                radiantLine.Values.AddRange(radiantPoints);
                                temperatureLine.Values.AddRange(tempPoints);
                                // energyLine.Values.AddRange(energyPoints);
                                trend.Update();
                            }
                        }
                        catch { }
                    });
                }
            }
            catch { }
        }

        private void _btnPrintMonthEnergy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Report.csv";
                sfd.Filter = "Csv files (*.csv)|*.csv";
                if (sfd.ShowDialog() == true)
                {
                    if (_dgMonthEnergy.ItemsSource is List<MinMaxEnergyRecord> source)
                    {
                        var csvString = CsvHelper.ToCsvString<EnergyReportModel>(source.Select(x =>
                        {
                            return new EnergyReportModel()
                            {
                                DateTime = $"{x.DateTime:yyyy/MM}",
                                Energy = x.Energy
                            };
                        }));
                        File.WriteAllText(sfd.FileName, csvString);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void _btnPrintDayEnergy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Report.csv";
                sfd.Filter = "Csv files (*.csv)|*.csv";
                if (sfd.ShowDialog() == true)
                {
                    if (_dgDayEnergy.ItemsSource is List<MinMaxEnergyRecord> source)
                    {
                        var csvString = CsvHelper.ToCsvString<EnergyReportModel>(source.Select(x =>
                        {
                            return new EnergyReportModel()
                            {
                                DateTime = $"{x.DateTime:yyyy-MM-dd}",
                                Energy = x.Energy
                            };
                        }));
                        File.WriteAllText(sfd.FileName, csvString);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void _btnRefreshMonthEnergy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                colSeriesMonth.Values = MonthTrendValues;
                _dgMonthEnergy.ItemsSource = null;

                if (_cobMonthEnergyDataSource.SelectedItem is EnergyTrendDataSource dataSource &&
                    _dtpMonthEnergy.SelectedDate != null)
                {
                    var monthRecords = (await GetMonthEnergyValues(dataSource, _dtpMonthEnergy.SelectedDate.Value))?.ToList();
                    Dispatcher.Invoke(() =>
                    {
                        if (monthRecords != null)
                        {
                            for (int i = 0; i < MonthTrendValues.Count; i++)
                            {
                                if (monthRecords.FirstOrDefault(x => x.DateTime.Month == i + 1) is MinMaxEnergyRecord record)
                                {
                                    MonthTrendValues[i].Y = record.Energy;
                                }
                                else
                                {
                                    MonthTrendValues[i].Y = 0;
                                }
                            }
                        }

                        _dgMonthEnergy.ItemsSource = monthRecords;
                    });
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
        }

        private async void _btnRefreshDayEnergy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                colSeriesDay.Values = DayTrendValues;
                _dgDayEnergy.ItemsSource = null;

                if (_cobDayEnergyDataSource.SelectedItem is EnergyTrendDataSource dataSource &&
                    _dtpDayEnergy.SelectedDate != null)
                {
                    var dayRecords = (await GetDayEnergyValues(dataSource, _dtpDayEnergy.SelectedDate.Value))?.ToList();
                    Dispatcher.Invoke(() =>
                    {
                        if (dayRecords != null)
                        {
                            if (DayTrendValues.Count != dayRecords.Count)
                            {
                                DayTrendValues.Clear();
                                DateTime firstDay = _dtpDayEnergy.SelectedDate.Value.GetFirstDayOfMonth();
                                DateTime lastDay = _dtpDayEnergy.SelectedDate.Value.GetLastDayOfMonth();
                                xTimeDay.MaxValue = lastDay.Day + 1;
                                xTimeDay.MinValue = 1;
                                for (int i = 1; i <= lastDay.Day; i++)
                                {
                                    colSeriesDay.Values.Add(new ObservablePoint(i, 0));
                                }
                            }

                            for (int i = 0; i < DayTrendValues.Count; i++)
                            {
                                if (dayRecords.FirstOrDefault(x => x.DateTime.Day == i + 1) is MinMaxEnergyRecord record)
                                {
                                    DayTrendValues[i].Y = record.Energy;
                                }
                                else
                                {
                                    DayTrendValues[i].Y = 0;
                                }
                            }
                        }

                        _dgDayEnergy.ItemsSource = dayRecords;
                    });
                }
            }
            catch (Exception ex)
            {

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

                return new DateTime((long)(x)).ToString("yyyy-MM-dd HH:mm");
            };
            //xTime.Separator.Step = TimeSpan.FromHours(1).Ticks;

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

            temperatureLine = new LineSeries()
            {
                PointGeometry = DefaultGeometries.None,
                Title = "Panel Temperature (°C)",
                FontSize = 16,
                ScalesYAt = 1,
                Stroke = new SolidColorBrush(System.Windows.Media.Colors.Orange),
                Values = new ChartValues<DateTimePoint>(),
                Fill = Brushes.Transparent,
            };

            //energyLine = new LineSeries()
            //{
            //    PointGeometry = DefaultGeometries.None,
            //    Title = "Energy (kWh)",
            //    FontSize = 16,
            //    Visibility= Visibility.Collapsed,
            //    ScalesYAt = 1,
            //    Stroke = new SolidColorBrush(System.Windows.Media.Colors.DeepSkyBlue),
            //    Values = new ChartValues<DateTimePoint>(),
            //    Fill = Brushes.Transparent,
            //};

            config = new CartesianMapper<DateTimePoint>().X(x =>
            {
                return x.DateTime.Ticks;
            }).Y(x =>
            {
                return x.Value;
            });

            trend.Series = new SeriesCollection(config);
            trend.Series.Add(powerLine);
            trend.Series.Add(radiantLine);
            trend.Series.Add(temperatureLine);
            // trend.Series.Add(energyLine);
            trend.MouseMove += OnTrendMouseMove;

            trend.DataTooltip.Background = new SolidColorBrush(Color.FromArgb(120, 128, 128, 128));
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


        private void OnChartMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                var chart = (CartesianChart)sender;
                //lets get where the mouse is at our chart
                var mouseCoordinate = e.GetPosition(chart);

                //now that we know where the mouse is, lets use
                //ConverToChartValues extension
                //it takes a point in pixes and scales it to our chart current scale/values
                var p = chart.ConvertToChartValues(mouseCoordinate);

                ////in the Y section, lets use the raw value
                //vm.YPointer = p.Y;

                //for X in this case we will only highlight the closest point.
                //lets use the already defined ClosestPointTo extension
                //it will return the closest ChartPoint to a value according to an axis.
                //here we get the closest point to p.X according to the X axis
                var series = chart.Series[0];
                var closetsPoint = series.ClosestPointTo(p.X, AxisOrientation.X);

                chart.AxisX[0].Sections[0].Value = closetsPoint.X;
            }
            catch { }
        }

        public async Task<IEnumerable<MinMaxEnergyRecord>> GetDayEnergyValues(EnergyTrendDataSource dataSource, DateTime time)
        {
            try
            {
                DateTime minTime = time.GetFirstDayOfMonth();
                DateTime maxTime = time.GetLastDayOfMonth();
                MySqlCompiler compiler = new MySqlCompiler();

                IServerConnector connector = ServerConnectorProvider.GetConnector(0);
                Query query = new Query().From(dataSource.TableName)
                    .Select("DateTime")
                    .SelectRaw($"min({dataSource.EnergyColumnName}) as `MinEnergy`")
                    .SelectRaw($"max({dataSource.EnergyColumnName}) as `MaxEnergy`")
                    .Where("DateTime", ">=", DateTime.Parse($"{minTime:yyyy-MM-dd 00:00:00}"))
                    .Where("DateTime", "<=", DateTime.Parse($"{maxTime:yyyy-MM-dd 23:59:59}"))
                    .WhereNotNull(dataSource.EnergyColumnName)
                    .GroupByRaw("DATE_FORMAT(DateTime, '%m%d')");
                var sqlResult = compiler.Compile(query);
                string sql = sqlResult.ToString();
                var items = await connector.Query<MinMaxEnergyRecord>("DB connections/solar_db", sql);
                return items;
            }
            catch { }
            return null;
        }

        public async Task<IEnumerable<MinMaxEnergyRecord>> GetMonthEnergyValues(EnergyTrendDataSource dataSource, DateTime time)
        {
            try
            {
                DateTime minTime = time.GetFirstDayOfYear();
                DateTime maxTime = time.GetLastDayOfYear();
                MySqlCompiler compiler = new MySqlCompiler();

                IServerConnector connector = ServerConnectorProvider.GetConnector(0);
                Query query = new Query().From(dataSource.TableName)
                    .Select("DateTime")
                    .SelectRaw($"min({dataSource.EnergyColumnName}) as `MinEnergy`")
                    .SelectRaw($"max({dataSource.EnergyColumnName}) as `MaxEnergy`")
                    .Where("DateTime", ">=", DateTime.Parse($"{minTime:yyyy-MM-dd 00:00:00}"))
                    .Where("DateTime", "<=", DateTime.Parse($"{maxTime:yyyy-MM-dd 23:59:59}"))
                    .WhereNotNull(dataSource.EnergyColumnName)
                    .GroupByRaw("DATE_FORMAT(DateTime, '%m')");
                var sqlResult = compiler.Compile(query);
                string sql = sqlResult.ToString();
                var items = await connector.Query<MinMaxEnergyRecord>("DB connections/solar_db", sql);
                return items;
            }
            catch { }
            return null;
        }
    }

    public class EnergyTrendDataSource
    {
        public string DisplayName { get; set; }
        public string TableName { get; set; }
        public string EnergyColumnName { get; set; } = "Energy";
    }

    public class SensorTrendDataSource
    {
        public string DisplayName { get; set; }
        public string TableName { get; set; }
        public string EnergyColumnName { get; set; } = "Energy";
        public string PowerColumnName { get; set; } = "Power";
    }

    public class EnergyReportModel
    {
        public string DateTime { get; set; }
        public double? Energy { get; set; }
    }
}
