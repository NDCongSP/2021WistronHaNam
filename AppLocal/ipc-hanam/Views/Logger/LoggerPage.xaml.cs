using ipc_hanam.models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ipc_hanam
{
    /// <summary>
    /// Interaction logic for LoggerPage.xaml
    /// </summary>
    public partial class LoggerPage : UserControl, IHaveTitle
    {
        public LoggerModel Logger { get; set; }
        public List<InverterOverviewView> InverterOverviewViews { get; set; }
        public List<PowerMeterOverviewView> PowerMeterOverviewViews { get; set; }
        public List<ElectricalQualityMeterOverviewView> ElectricalQualityMeterOverviewViews { get; set; }
        public List<ZeroExportPowerMeterOverviewView> ZeroExportPowerMeterOverviewViews { get; set; }
        public string Title { get; }
        public Uri IconSource { get; }

        public LoggerPage(LoggerModel logger)
        {
            InitializeComponent();
            Logger = logger;
            InverterOverviewViews = new List<InverterOverviewView>();
            PowerMeterOverviewViews = new List<PowerMeterOverviewView>();
            ElectricalQualityMeterOverviewViews = new List<ElectricalQualityMeterOverviewView>();
            ZeroExportPowerMeterOverviewViews = new List<ZeroExportPowerMeterOverviewView>();
            Title = $"{logger.DisplayName}";

            _invStringView.LoadLogger(logger);

            foreach (var inverter in Logger.Inverters)
            {
                var view = CreateInverterOverviewView(inverter);
                view.Width = 600;
                view.Margin = new Thickness(16);
                InverterOverviewViews.Add(view);
                _wrapPanel.Children.Add(view);
            }

            if (Logger.EnergyPowerMeter != null)
            {
                PowerMeterOverviewView pmOverview = new PowerMeterOverviewView(logger, Logger.EnergyPowerMeter);
                pmOverview.Width = 600;
                pmOverview.Margin = new Thickness(16);
                PowerMeterOverviewViews.Add(pmOverview);
                _pmWrapPanel.Children.Add(pmOverview);
            } 


            if (Logger.ElectricalQualityMeter != null)
            {
                ElectricalQualityMeterOverviewView view = new ElectricalQualityMeterOverviewView(logger, Logger.ElectricalQualityMeter);
                view.Width = 600;
                view.Margin = new Thickness(16);
                ElectricalQualityMeterOverviewViews.Add(view);
                _pmWrapPanel.Children.Add(view);
            }

            if (Logger.ZeroExportPowerMeter != null)
            {
                ZeroExportPowerMeterOverviewView view = new ZeroExportPowerMeterOverviewView(logger, Logger.ZeroExportPowerMeter);
                view.Width = 600;
                view.Margin = new Thickness(16);
                ZeroExportPowerMeterOverviewViews.Add(view);
                _pmWrapPanel.Children.Add(view);
            }

            int index = 1;
            foreach (var invView in InverterOverviewViews)
            {
                MccbView mccbView = new MccbView(invView.Inverter, invView.InverterPage, index);
                _mccbStack.Children.Add(mccbView);
                index++;
            }
            _kn612.PreviewMouseDown += _kn612_PreviewMouseDown;
            _pm.PreviewMouseDown += _pm_PreviewMouseDown;
            Logger.PropertyChanged += Logger_PropertyChanged;

            DataContext = this;
        }

        private void Logger_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e != null)
                {
                    if (e.PropertyName == nameof(LoggerModel.ACBStatus))
                    {
                        _acbLabel.Content = Logger.ACBStatus;
                        switch (Logger.ACBStatus)
                        {
                            case "Opened":
                                _acbLabel.Foreground = Brushes.Green;
                                break;
                            case "Closed":
                                _acbLabel.Foreground = Brushes.Red;
                                break;
                            case "Trip":
                                _acbLabel.Foreground = Brushes.Orange;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    _acbLabel.Content = Logger.ACBStatus;
                    switch (Logger.ACBStatus)
                    {
                        case "Opened":
                            _acbLabel.Foreground = Brushes.Green;
                            break;
                        case "Closed":
                            _acbLabel.Foreground = Brushes.Red;
                            break;
                        case "Trip":
                            _acbLabel.Foreground = Brushes.Orange;
                            break;
                        default:
                            break;
                    }
                }
            });
        }

        private void _pm_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainPage.Instance.NavigateTo(PowerMeterOverviewViews[0].PowerMeterPage);
        }

        private void _kn612_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainPage.Instance.NavigateTo(ElectricalQualityMeterOverviewViews[0].ElectricalQualityMeterPage);
        }

        private InverterOverviewView CreateInverterOverviewView(InverterModel inverter)
        {
            InverterOverviewView view = new InverterOverviewView(Logger, inverter);
            return view;
        }
    }
}
