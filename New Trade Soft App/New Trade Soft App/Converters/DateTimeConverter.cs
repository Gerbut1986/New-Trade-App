namespace New_Trade_Soft_App.Converters
{
    using System;
    using System.Text;
    using System.Windows.Data;
    using System.Globalization;

    public class DateTimeConverter : IValueConverter
    {
        public bool ShowFfffff { get; set; } = true;
        public bool ShowSeconds { get; set; } = true;
        public bool ShowHHmm { get; set; } = true;
        public bool ShowYyyyMMdd { get; set; } = false;
        public int MinYear { get; set; } = 1900;
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DateTime time = (DateTime)value;
                if (time.Year >= MinYear)
                {
                    StringBuilder sb = new StringBuilder();
                    if (ShowYyyyMMdd)
                    {
                        sb.Append("yyyy.MM.dd");
                        if (ShowHHmm) sb.Append(" ");
                    }
                    if (ShowHHmm)
                    {
                        sb.Append("HH:mm");
                        if (ShowSeconds) sb.Append(":");
                    }
                    if (ShowSeconds)
                    {
                        sb.Append("ss");
                        if (ShowFfffff) sb.Append(".");
                    }
                    if (ShowFfffff)
                    {
                        sb.Append("ffffff");
                    }
                    return time.ToString(sb.ToString());
                }
            }
            catch { }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
