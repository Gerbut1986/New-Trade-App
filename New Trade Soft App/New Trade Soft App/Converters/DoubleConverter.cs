namespace New_Trade_Soft_App.Converters
{
    using System;
    using System.Windows.Data;
    using System.Globalization;

    public class DoubleConverter : IValueConverter
    {
        public string ZeroResult = "0";
        public string Format { get; set; } = "F2";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double res = (double)value;
                if (res != 0)
                {
                    return res.ToString(Format, CultureInfo.InvariantCulture);
                }
            }
            catch { }

            return ZeroResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
