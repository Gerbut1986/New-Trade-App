namespace New_Trade_Soft_App.Converters
{
    using System;
    using System.Windows.Data;
    using System.Globalization;

    public class SelectedValueNotNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
