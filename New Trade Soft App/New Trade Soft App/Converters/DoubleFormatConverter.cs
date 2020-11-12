namespace New_Trade_Soft_App.Converters
{
    using System;
    using System.Windows.Data;
    using System.Globalization;

    public class DoubleFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //double value = (double)values[0];
                //if (!double.IsNaN(value))
                //{
                //    string format = (string)values[1];
                //    return value.ToString(format, CultureInfo.InvariantCulture);
                //}
                var trade = new Models.TradeModel();//values[0] as Classes.TradeModel;
                double price = (double)values[0];
                if (price != 0)
                {
                    return trade.FormatPrice(price);
                }
            }
            catch { }

            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
