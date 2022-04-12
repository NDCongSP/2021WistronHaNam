using AFSClient;
using ipc_hanam.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ElectricalQualityMeterPage.xaml
    /// </summary>
    public partial class ElectricalQualityMeterPage : UserControl, IHaveTitle
    {
        public ElectricalQualityMeterModel PowerMeter { get; set; }
        public List<TagRowValue> TagRowValues { get; set; }

        public LoggerModel Logger { get; set; }
        public string Title { get; }
        public Uri IconSource { get; }

        public ElectricalQualityMeterPage(LoggerModel logger, ElectricalQualityMeterModel powerMeter)
        {
            InitializeComponent();
            Logger = logger;
            PowerMeter = powerMeter;

            Title = $"{logger.DisplayName} / {powerMeter.DisplayName}";
            TagRowValues = new List<TagRowValue>();

            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(powerMeter))
            {
                if (pd.PropertyType == typeof(TagNode))
                {
                    TagNode tag = pd.GetValue(powerMeter) as TagNode;

                    if (tag != null)
                    {
                        TagRowValue tagRowValue = new TagRowValue(tag, tag.Description);
                        TagRowValues.Add(tagRowValue);
                    }
                }
            }
            DataContext = this;
        }
    }
}
