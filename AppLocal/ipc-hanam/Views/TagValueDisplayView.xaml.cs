using AFSClient;
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
    /// Interaction logic for TagValueDisplayView.xaml
    /// </summary>
    public partial class TagValueDisplayView : UserControl
    {
        public TagNode TagNode { get; set; }
        public string Title { get; set; }

        public TagValueDisplayView(TagNode tag, string title)
        {
            InitializeComponent();
            TagNode = tag;
            Title = title;
            DataContext = this;

            if (tag != null)
            {
                _label.LinkedTagChanged += _label_LinkedTagChanged;
                _label.ResolvedPathChanged += _label_ResolvedPathChanged;
                _label.TagPath = $"{tag.ServerIndex}/{TagNode.Path}";
                _label.ConverterParameter = tag.Unit;
            }
        }

        private void _label_ResolvedPathChanged(object sender, EventArgs e)
        {
            string resolved = _label.ResolvedPath;
        }

        private void _label_LinkedTagChanged(object sender, EventArgs e)
        {
            var tag = _label.LinkedTag;
        }
    }
}
