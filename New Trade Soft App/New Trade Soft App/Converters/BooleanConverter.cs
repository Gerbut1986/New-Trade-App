namespace New_Trade_Soft_App.Converters
{
    using System;
    using System.Windows.Data;
    using System.Globalization;
    public class BooleanConverter : IValueConverter
    {
        public bool Inverse { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool bValue = (bool)value;
                return Inverse ? !bValue : bValue;
            }
            catch { }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
