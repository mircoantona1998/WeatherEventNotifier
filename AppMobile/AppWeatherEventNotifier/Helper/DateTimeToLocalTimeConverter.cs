using System;
namespace AppWeatherEventNotifier.Helper
{
    public class DateTimeToLocalTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
