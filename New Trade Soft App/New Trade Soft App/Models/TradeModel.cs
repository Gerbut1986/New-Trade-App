namespace New_Trade_Soft_App.Models
{
    using System.Globalization;
    using System.Windows.Media;
    using System.Xml.Serialization;

    public class TradeModel : BaseModel
    {
        string _LastLog;
        [XmlIgnore]
        public string LastLog
        {
            get { return _LastLog; }
            set { if (_LastLog != value) { _LastLog = value; OnPropertyChanged(); } }
        }

        Brush _LastLogBrush;
        [XmlIgnore]
        public Brush LastLogBrush
        {
            get { return _LastLogBrush; }
            set { if (_LastLogBrush != value) { _LastLogBrush = value; OnPropertyChanged(); } }
        }

        [XmlIgnore]
        public string PriceFormat { get; private set; }

        public string FormatPrice(double price)
        {
            return /*PriceFormat.Length > 0 ? price.ToString(PriceFormat, CultureInfo.InvariantCulture) :*/ price.ToString(CultureInfo.InvariantCulture);
        }
    }
}
