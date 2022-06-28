using LiveCharts.Defaults;
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
using LiveCharts.Wpf;
using ipc_hanam.models;
using LiveCharts.Helpers;
using AFSClient;
using System.Windows.Threading;
using SqlKata;
using SqlKata.Compilers;

namespace ipc_hanam
{
    /// <summary>
    /// Interaction logic for OverviewPage.xaml
    /// </summary>
    public partial class OverviewPage : UserControl
    {
        public ChartValues<ObservablePoint> DayTrendValues { get; set; } = new ChartValues<ObservablePoint>();
        public ChartValues<ObservablePoint> MonthTrendValues { get; set; } = new ChartValues<ObservablePoint>();
        public ChartValues<ObservablePoint> YearTrendValues { get; set; } = new ChartValues<ObservablePoint>();
        private System.Timers.Timer _refreshTimer;

        public OverviewPage()
        {
            InitializeComponent();

            #region Init day chart
            dayChart.DataTooltip.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            dayChart.MouseMove += OnChartMouseMove;
            dayChart.MouseLeave += DayChart_MouseLeave;
            colSeriesDay.Values = DayTrendValues;
            Random rnd = new Random();
            for (int i = 0; i < 24; i++)
            {
                colSeriesDay.Values.Add(new ObservablePoint(i, 0));
            }
            xDayAxisSection.Value = DateTime.Now.Hour;
            #endregion

            #region Init month chart
            monthChart.DataTooltip.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            monthChart.MouseMove += OnChartMouseMove;
            monthChart.MouseLeave += MonthChart_MouseLeave;
            DateTime firstDay = DateTime.Now.GetFirstDayOfMonth();
            DateTime lastDay = DateTime.Now.GetLastDayOfMonth();
            xTimeMonth.MaxValue = lastDay.Day + 1;
            xTimeMonth.MinValue = 1;
            colSeriesMonth.Values = MonthTrendValues;
            for (int i = 1; i <= lastDay.Day; i++)
            {
                colSeriesMonth.Values.Add(new ObservablePoint(i, 0));
            }
            xMonthAxisSection.Value = DateTime.Now.Day;
            #endregion

            #region Init year chart
            yearChart.DataTooltip.Background = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            yearChart.MouseMove += OnChartMouseMove;
            yearChart.MouseLeave += YearChart_MouseLeave;
            colSeriesYear.Values = YearTrendValues;
            xTimeYear.MaxValue = 13;
            xTimeYear.MinValue = 1;
            for (int i = 1; i <= 12; i++)
            {
                colSeriesYear.Values.Add(new ObservablePoint(i, 0));
            }
            xYearAxisSection.Value = DateTime.Now.Month;
            #endregion

            _refreshTimer = new System.Timers.Timer();
            _refreshTimer.Interval = TimeSpan.FromMinutes(5).TotalMilliseconds;
            _refreshTimer.Elapsed += _refreshTimer_Elapsed;
            _refreshTimer_Elapsed(null, null);

            DataContext = this;

            IoC.Instance.yearly_energy_yields.ValueChanged += Yearly_energy_yields_ValueChanged;
        }

        private void Yearly_energy_yields_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
        }

        private void YearChart_MouseLeave(object sender, MouseEventArgs e)
        {
            var chart = (CartesianChart)sender;
            chart.AxisX[0].Sections[0].Value = DateTime.Now.Month;
        }

        private void MonthChart_MouseLeave(object sender, MouseEventArgs e)
        {
            var chart = (CartesianChart)sender;
            chart.AxisX[0].Sections[0].Value = DateTime.Now.Day;
        }

        private void DayChart_MouseLeave(object sender, MouseEventArgs e)
        {
            var chart = (CartesianChart)sender;
            chart.AxisX[0].Sections[0].Value = DateTime.Now.Hour;
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

        private void _refreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _refreshTimer.Stop();
            try
            {
                RefreshTrend();
            }
            catch
            {

            }
            _refreshTimer.Start();
        }


        public async void RefreshTrend()
        {
            var hourRecords = (await GetHourEnergyValues())?.ToList();
            var dayRecords = (await GetDayEnergyValues())?.ToList();
            var monthRecords = (await GetMonthEnergyValues())?.ToList();

            double monthEnergy = 0;
            if (dayRecords != null)
            {
                monthEnergy = dayRecords.Sum(x => x.Energy);
            }

            double yearEnergy = 0;
            if (monthRecords != null)
            {
                yearEnergy = monthRecords.Sum(x => x.Energy);
            }

            try
            {
                if (IoC.Instance.ServerConnector.IsStarted)
                {
                    IoC.Instance.monthly_energy_yields.WriteWithoutResponse(monthEnergy, 3);
                    IoC.Instance.yearly_energy_yields.WriteWithoutResponse(yearEnergy, 3);

                    double daily_energy_yields = 0;
                }
            }
            catch { }

            Dispatcher.Invoke(() =>
            {
                if (hourRecords != null)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        if (hourRecords.FirstOrDefault(x => x.DateTime.Hour == i) is MinMaxEnergyRecord record)
                        {
                            DayTrendValues[i].Y = record.Energy;
                        }
                        else
                        {
                            DayTrendValues[i].Y = 0;
                        }
                    }
                }

                if (dayRecords != null)
                {
                    if (MonthTrendValues.Count != dayRecords.Count)
                    {
                        MonthTrendValues.Clear();
                        DateTime firstDay = DateTime.Now.GetFirstDayOfMonth();
                        DateTime lastDay = DateTime.Now.GetLastDayOfMonth();
                        xTimeMonth.MaxValue = lastDay.Day + 1;
                        xTimeMonth.MinValue = 1;
                        for (int i = 1; i <= lastDay.Day; i++)
                        {
                            colSeriesMonth.Values.Add(new ObservablePoint(i, 0));
                        }
                    }

                    for (int i = 0; i < MonthTrendValues.Count; i++)
                    {
                        if (dayRecords.FirstOrDefault(x => x.DateTime.Day == i + 1) is MinMaxEnergyRecord record)
                        {
                            MonthTrendValues[i].Y = record.Energy;
                        }
                        else
                        {
                            MonthTrendValues[i].Y = 0;
                        }
                    }
                }

                if (monthRecords != null)
                {
                    for (int i = 0; i < YearTrendValues.Count; i++)
                    {
                        if (monthRecords.FirstOrDefault(x => x.DateTime.Month == i + 1) is MinMaxEnergyRecord record)
                        {
                            YearTrendValues[i].Y = record.Energy;
                        }
                        else
                        {
                            YearTrendValues[i].Y = 0;
                        }
                    }
                }
            });
        }

        public async Task<IEnumerable<MinMaxEnergyRecord>> GetHourEnergyValues()
        {
            try
            {
                DateTime time = DateTime.Now;
                IServerConnector connector = ServerConnectorProvider.GetConnector(0);

                MySqlCompiler compiler = new MySqlCompiler();
                Query query = new Query().From("summary")
                    .Select("DateTime")
                    .SelectRaw("min(Energy) as `MinEnergy`")
                    .SelectRaw("max(Energy) as `MaxEnergy`")
                    .Where("DateTime", ">=", DateTime.Parse($"{time:yyyy-MM-dd 00:00:00}"))
                    .Where("DateTime", "<=", DateTime.Parse($"{time:yyyy-MM-dd 23:59:59}"))
                    .WhereNotNull("Energy")
                    .GroupByRaw("DATE_FORMAT(DateTime, '%m%d%H')");
                var sqlResult = compiler.Compile(query);
                string sql = sqlResult.ToString();
                var items = await connector.Query<MinMaxEnergyRecord>("DB connections/solar_db", sql);
                return items;
            }
            catch { }
            return null;
        }

        public async Task<IEnumerable<MinMaxEnergyRecord>> GetDayEnergyValues()
        {
            try
            {
                DateTime minTime = DateTime.Now.GetFirstDayOfMonth();
                DateTime maxTime = DateTime.Now.GetLastDayOfMonth();
                MySqlCompiler compiler = new MySqlCompiler();

                IServerConnector connector = ServerConnectorProvider.GetConnector(0);
                Query query = new Query().From("summary")
                    .Select("DateTime")
                    .SelectRaw("min(Energy) as `MinEnergy`")
                    .SelectRaw("max(Energy) as `MaxEnergy`")
                    .Where("DateTime", ">=", DateTime.Parse($"{minTime:yyyy-MM-dd 00:00:00}"))
                    .Where("DateTime", "<=", DateTime.Parse($"{maxTime:yyyy-MM-dd 23:59:59}"))
                    .Where("Energy", "!=", 0)
                    .WhereNotNull("Energy")
                    .GroupByRaw("DATE_FORMAT(DateTime, '%m%d')");
                var sqlResult = compiler.Compile(query);
                string sql = sqlResult.ToString();
                var items = await connector.Query<MinMaxEnergyRecord>("DB connections/solar_db", sql);
                return items;
            }
            catch { }
            return null;
        }

        public async Task<IEnumerable<MinMaxEnergyRecord>> GetMonthEnergyValues()
        {
            try
            {
                DateTime minTime = DateTime.Now.GetFirstDayOfYear();
                DateTime maxTime = DateTime.Now.GetLastDayOfYear();
                MySqlCompiler compiler = new MySqlCompiler();

                IServerConnector connector = ServerConnectorProvider.GetConnector(0);
                Query query = new Query().From("summary")
                    .Select("DateTime")
                    .SelectRaw("min(Energy) as `MinEnergy`")
                    .SelectRaw("max(Energy) as `MaxEnergy`")
                    .Where("DateTime", ">=", DateTime.Parse($"{minTime:yyyy-MM-dd 00:00:00}"))
                    .Where("DateTime", "<=", DateTime.Parse($"{maxTime:yyyy-MM-dd 23:59:59}"))
                    .Where("Energy", "!=", 0)
                    .WhereNotNull("Energy")
                    .GroupByRaw("DATE_FORMAT(DateTime, '%m')");
                var sqlResult = compiler.Compile(query);
                string sql = sqlResult.ToString();
                var items = await connector.Query<MinMaxEnergyRecord>("DB connections/solar_db", sql);
                return items;
            }
            catch { }
            return null;
        }

        public async Task<IEnumerable<MinMaxEnergyRecord>> GetYearEnergyValues()
        {
            try
            {
                DateTime minTime = DateTime.Now.GetFirstDayOfYear();
                DateTime maxTime = DateTime.Now.GetLastDayOfYear();
                MySqlCompiler compiler = new MySqlCompiler();

                IServerConnector connector = ServerConnectorProvider.GetConnector(0);
                Query query = new Query().From("summary")
                    .Select("DateTime")
                    .SelectRaw("min(Energy) as `MinEnergy`")
                    .SelectRaw("max(Energy) as `MaxEnergy`")
                    .Where("DateTime", ">=", DateTime.Parse($"{minTime:yyyy-MM-dd 00:00:00}"))
                    .Where("DateTime", "<=", DateTime.Parse($"{maxTime:yyyy-MM-dd 23:59:59}"))
                    .WhereNotNull("Energy")
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

    public class MinMaxEnergyRecord
    {
        public DateTime DateTime { get; set; }
        public double? MinEnergy { get; set; }
        public double? MaxEnergy { get; set; }

        public double Energy
        {
            get
            {
                if (MinEnergy != null && MaxEnergy != null)
                {
                    return Math.Truncate(Math.Abs(MaxEnergy.Value - MinEnergy.Value));
                }
                return 0;
            }
        }
    }

}
