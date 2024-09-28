using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace StagesView
{
    public class HexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Media.Color color = Colors.White;
            string hexColor = value as string;
            if (hexColor == null)
            {
                return color;
            }

            if (hexColor[0] == '#')
            {
                hexColor = hexColor.Substring(1);
            }

            if (hexColor.Length == 8)
            {
                color.A = (byte)Int32.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber);
                color.R = (byte)Int32.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber);
                color.G = (byte)Int32.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber);
                color.B = (byte)Int32.Parse(hexColor.Substring(6, 2), NumberStyles.HexNumber);
            }
            else if (hexColor.Length == 6)
            {
                color.R = (byte)Int32.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber);
                color.G = (byte)Int32.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber);
                color.B = (byte)Int32.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber);
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
