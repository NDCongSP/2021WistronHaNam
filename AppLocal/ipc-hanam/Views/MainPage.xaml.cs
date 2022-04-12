using AFS.HMILibrary.Wpf;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public static MainPage Instance { get; protected set; }

        private OverviewPage _overviewPage;
        private LayoutPage _layoutPage;
        private DevicesPage _devicesPage;
        private PowerMetersPage _powerMetersPage;
        private TrendsPage _trendsPage;
        private AlarmsPage _alarmsPage;
        private SettingsPage _settingsPage;

        public MainPage()
        {
            InitializeComponent();

            _overviewPage = new OverviewPage();
            _layoutPage = new LayoutPage();
            _devicesPage = new DevicesPage();
            _powerMetersPage = new PowerMetersPage();
            _trendsPage = new TrendsPage();
            _alarmsPage = new AlarmsPage();
            _settingsPage = new SettingsPage();

            foreach (var logger in IoC.Instance.Station.Loggers)
            {
                var loggerPage = _devicesPage.LoggerOverviewViews.FirstOrDefault(x => x.Logger == logger).LoggerPage;
                NavigationViewItem loggerMenuItem = new NavigationViewItem()
                {
                    Content = logger.DisplayName,
                    Tag = loggerPage,
                };

                //var overviewMenu = new NavigationViewItem()
                //{
                //    Content = "Overview",
                //};
                //loggerMenuItem.MenuItems.Add(overviewMenu);

                if (logger.EnergyPowerMeter != null)
                {
                    var page = loggerPage.PowerMeterOverviewViews.FirstOrDefault(x => x.PowerMeter == logger.EnergyPowerMeter).PowerMeterPage;
                    NavigationViewItem menuItem = new NavigationViewItem()
                    {
                        Content = logger.EnergyPowerMeter.DisplayName,
                        Tag = page,
                    };
                    loggerMenuItem.MenuItems.Add(menuItem);
                }

                if (logger.ElectricalQualityMeter != null)
                {
                    var page = loggerPage.ElectricalQualityMeterOverviewViews.FirstOrDefault(x => x.PowerMeter == logger.ElectricalQualityMeter).ElectricalQualityMeterPage;
                    NavigationViewItem menuItem = new NavigationViewItem()
                    {
                        Content = logger.ElectricalQualityMeter.DisplayName,
                        Tag = page,
                    };
                    loggerMenuItem.MenuItems.Add(menuItem);
                }

                if (logger.ZeroExportPowerMeter != null)
                {
                    var page = loggerPage.ZeroExportPowerMeterOverviewViews.FirstOrDefault(x => x.PowerMeter == logger.ZeroExportPowerMeter).PowerMeterPage;
                    NavigationViewItem menuItem = new NavigationViewItem()
                    {
                        Content = logger.ZeroExportPowerMeter.DisplayName,
                        Tag = page,
                    };
                    loggerMenuItem.MenuItems.Add(menuItem);
                }


                foreach (var inverter in logger.Inverters)
                {
                    var inverterPage = loggerPage.InverterOverviewViews.FirstOrDefault(x => x.Inverter == inverter).InverterPage;
                    NavigationViewItem inverterMenuItem = new NavigationViewItem()
                    {
                        Content = inverter.DisplayName,
                        // Icon = new BitmapIcon() { UriSource = new Uri("/Resources/Images/icons8-dynamo-96.png.crdownload") }
                        Tag = inverterPage
                    };
                    loggerMenuItem.MenuItems.Add(inverterMenuItem);
                }
                _menuDeviceManagement.MenuItems.Add(loggerMenuItem);
            }

            _mainNavigation.SelectionChanged += _mainNavigation_SelectionChanged;
            _mainNavigation.SelectedItem = _mainNavigation.MenuItems[0];

            Instance = this;
        }

        private void _mainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                NavigateTo("Settings", null);
            }
            else
            {
                if (args.SelectedItem is NavigationViewItem item)
                {
                    if (item.Tag is UserControl content)
                    {
                        NavigateTo(content);
                    }
                    else
                    {
                        NavigateTo($"{item.Tag}", item);
                    }
                }
            }
        }

        public void NavigateTo(string page, NavigationViewItem item)
        {
            try
            {
                if (page == "Settings")
                {
                    HeaderModel headerModel = new HeaderModel()
                    {
                        DisplayName = $"Settings",
                    };
                    _mainNavigation.Header = headerModel;
                }
                else
                {
                    HeaderModel headerModel = new HeaderModel()
                    {
                        DisplayName = $"{item.Content}",
                        IconSource = (item.Icon as BitmapIcon)?.UriSource,
                    };
                    _mainNavigation.Header = headerModel;
                }

                switch (page)
                {
                    case "Overview":
                        _mainPageFrame.Content = _overviewPage;
                        break;
                    case "Layout":
                        _mainPageFrame.Content = _layoutPage;
                        break;
                    case "Devices":
                        _mainPageFrame.Content = _devicesPage;
                        break;
                    case "PowerMeters":
                        _mainPageFrame.Content = _powerMetersPage;
                        break;
                    case "Trends":
                        _mainPageFrame.Content = _trendsPage;
                        break;
                    case "Alarms":
                        _mainPageFrame.Content = _alarmsPage;
                        break;
                    case "Settings":
                        _mainPageFrame.Content = _settingsPage;
                        break;
                    default:
                        _mainPageFrame.Content = null;
                        break;
                }
            }
            catch { }
        }

        public void NavigateTo(object content)
        {
            try
            {
                string header = string.Empty;
                Uri iconSource = null;

                if (content is IHaveTitle ihaveTitle)
                {
                    header = ihaveTitle.Title;
                    iconSource = ihaveTitle.IconSource;
                }

                _mainNavigation.Header = new HeaderModel()
                {
                    DisplayName = header,
                    IconSource = iconSource
                };
                _mainPageFrame.Content = content;

                if (content != null)
                {
                    foreach (var item in GetNavigationViewItems())
                    {
                        if (item.Tag == content)
                        {
                            _mainNavigation.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            catch { }
        }

        public IEnumerable<NavigationViewItem> GetNavigationViewItems()
        {
            foreach (NavigationViewItem item in _mainNavigation.MenuItems)
            {
                yield return item;

                foreach (var childItem in GetNavigationViewItems(item))
                {
                    yield return childItem;
                }
            }
        }

        public IEnumerable<NavigationViewItem> GetNavigationViewItems(NavigationViewItem viewItem)
        {
            foreach (NavigationViewItem item in viewItem.MenuItems)
            {
                yield return item;
                foreach (var childItem in GetNavigationViewItems(item))
                {
                    yield return childItem;
                }
            }
        }
    }

    public class HeaderModel
    {
        public string DisplayName { get; set; }
        public Uri IconSource { get; set; }
    }
}
