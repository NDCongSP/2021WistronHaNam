using AFSClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ipc_hanam.models
{
    public class TagRowValue : INotifyPropertyChanged
    {
        private TagNode _originTag;
        private string _displayName;
        private string _value;
        private string _unit;

        public string DisplayName
        {
            get => _displayName;
            set
            {
                if (_displayName != value)
                {
                    _displayName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Unit
        {
            get => _unit;
            set
            {
                if (_unit != value)
                {
                    _unit = value;
                    RaisePropertyChanged();
                }
            }
        }

        public TagRowValue(TagNode tagNode, string displayName)
        {
            _originTag = tagNode;
            DisplayName = displayName;
            if (tagNode != null)
            {
                Value = tagNode.Value;
                Unit = tagNode.Unit;
                tagNode.ValueChanged += TagNode_ValueChanged;
                tagNode.QualityChanged += TagNode_QualityChanged;
            }
        }

        private void TagNode_QualityChanged(object sender, TagQualityChangedEventArgs e)
        {

        }

        private void TagNode_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e != null)
            {
                Value = e.NewValue;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
