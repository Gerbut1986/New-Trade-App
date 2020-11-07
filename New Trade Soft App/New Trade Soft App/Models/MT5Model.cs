namespace New_Trade_Soft_App.Models
{
    using System;
    using System.Xml.Serialization;

    public class MT5Model : BaseModel
    {
        #region Fields:
        double ask, bid, last, point, prevBid, prevAsk, prevLast;
        string symbol, username, password, address;
        DateTime time;
        #endregion

        #region Constructors:
        public MT5Model(string symbol)
        {
            Symbol = symbol;
            Time = DateTime.MinValue;
        }
        #endregion

        #region Properties:

        public string Symbol
        {
            get { return symbol; }
            set { if (symbol != value) { symbol = value; OnPropertyChanged(); } }
        }

        [XmlIgnore]
        public double Ask
        {
            get { return ask; }
            set { if (ask != value) { ask = value; OnPropertyChanged(); } }
        }

        [XmlIgnore]
        public double Bid
        {
            get { return bid; }
            set { if (bid != value) { bid = value; OnPropertyChanged(); } }
        }

        [XmlIgnore]
        public double PrevAsk
        {
            get { return prevAsk; }
            set { if (prevAsk != value) { prevAsk = value; OnPropertyChanged(); } }
        }

        [XmlIgnore]
        public double PrevBid
        {
            get { return prevBid; }
            set { if (prevBid != value) { prevBid = value; OnPropertyChanged(); } }
        }

        [XmlIgnore]
        public double PrevLast
        {
            get { return prevLast; }
            set { if (prevLast != value) { prevLast = value; OnPropertyChanged(); } }
        }

        [XmlIgnore]
        public DateTime Time
        {
            get { return time; }
            set { if (time != value) { time = value; OnPropertyChanged(); } }
        }

        [XmlIgnore]
        public int FlowBid { get; set; }

        [XmlIgnore]
        public int FlowAsk { get; set; }

        [XmlIgnore]
        public double Last
        {
            get { return last; }
            set { if (last != value) { last = value; OnPropertyChanged(); } }
        }

        #endregion

        #region Connection:
        [XmlIgnore]
        public mtapi.mt5.MT5API Api { get; set; }
        #endregion

        #region Methods:
        public bool CopyToPrev()
        {
            bool res = Bid != PrevBid || Ask != PrevAsk;
            PrevBid = Bid;
            PrevAsk = Ask;
            return res;
        }

        public virtual bool IsValid()
        {
            if (Bid == 0) return false;
            if (Ask == 0) return false;
            if (Bid > Ask) return false;
            return true;
        }
        #endregion
    }
}
