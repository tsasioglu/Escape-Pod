using System;
using System.Globalization;
using System.Windows.Data;

namespace EscapePod.Converters
{
    public class LongDateToShortDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string longDate = value as string;
            if (String.IsNullOrWhiteSpace(longDate))
            {
                return value;
            }

            DateTime longDateTime;
            if (!DateTime.TryParse(longDate.Substring(5, longDate.Length - 5), out longDateTime))
            {
                return value;
            }
            
            return longDateTime.ToLongDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
