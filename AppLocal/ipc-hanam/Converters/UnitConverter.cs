using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ipc_hanam
{
    public class UnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                if (double.TryParse(value?.ToString(), out double dValue))
                {
                    string unit = parameter.ToString();

                    if (unit.ToLower().StartsWith("k"))
                    {
                        if (dValue >= 1000)
                            return $"{(dValue / 1000.0f).ToString("f2")} M{unit.Remove(0, 1)}";
                    }
                    else
                    {
                        if (dValue >= 1000)
                            return $"{(dValue / 1000.0f).ToString("f1")} k{parameter}";
                        if (dValue >= 1000000)
                            return $"{(dValue / 1000.0f).ToString("f2")} M{parameter}";
                    }
                    return $"{dValue.ToString()} {parameter}";
                }
                return $"{value} {parameter}";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
