namespace New_Trade_Soft_App.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Globalization;

    public class BoolToVisibilityConverter : IValueConverter
    {
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool bValue = (bool)value;
                if (bValue)
                    return Inverse ? Visibility.Collapsed : Visibility.Visible;
            }
            catch { }

            return Inverse ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
