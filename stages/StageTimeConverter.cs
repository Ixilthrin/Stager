using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StagesView
{
    internal class StageTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "---";
            }

            TimeSpan span = (TimeSpan)value;

            if (span.TotalSeconds < 1 && span.TotalMilliseconds != 0)
            {
                return span.Milliseconds.ToString() + "ms";
            }

            return ((int)span.TotalSeconds).ToString() + "s";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
