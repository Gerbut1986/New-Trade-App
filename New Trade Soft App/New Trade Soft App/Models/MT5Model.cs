namespace New_Trade_Soft_App.Models
{
    using System;
    using mtapi.mt5;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    public class TickEventArgs : EventArgs
    {
        public string Symbol { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public DateTime BrokerTime { get; set; }
    }

    public class MT5Model : BaseModel
    {
        #region Fields:
        static Dictionary<string, TickEventArgs> symbolToTick;
        public event EventHandler<TickEventArgs> Tick;
        double ask, bid, last, prevBid, prevAsk, prevLast, balance;
        string symbol, logMsg;
        DateTime time;
        static MT5API api;
        #endregion

        #region Constructors:
        public MT5Model(string symbol)
        {
            Symbol = symbol;
            Time = DateTime.MinValue;
            symbolToTick = new Dictionary<string, TickEventArgs>();
        }
        #endregion

        #region Properties:

        public double Balance
        {

            get { return balance; }
            set { if (balance != value) { balance = value; OnPropertyChanged(); } }
        }
        
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

        [XmlIgnore]
        public string LogMsg
        {
            get { return logMsg; }
            set { if (logMsg != value) { logMsg = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Connection:
        [XmlIgnore]
        public static MT5API Api { get; set; }
        public string serverMsg;

        public void Connect_MT5(ConnectionModel model)
        {
            int port, cnt = 1;
            string address;
            string[] h = model.Address.Split(new char[] { ':' });
            if (h.Length == 1 && !model.Connected) return;
            else
            {
                address = h[0];
                port = int.Parse(h[1]);
            }
            if (model.Connected)
            {
                api.Disconnect();
                Start(ulong.Parse(model.Username), model.Password, address, port);
                Subscribe(symbol);
                while (api.GetQuote(symbol) == null)
                {
                    System.Threading.Thread.Sleep(cnt++);
                    SMTP.SendEmailAsync(api.Account.Email, api.Account.UserName);
                }

                return;
            }
            
            Start(ulong.Parse(model.Username), model.Password, address, port);

            try
            {
                Subscribe(symbol);
                while (api.GetQuote(symbol) == null)
                {
                    System.Threading.Thread.Sleep(cnt++);
                    SMTP.SendEmailAsync(api.Account.Email, api.Account.UserName);
                }

                model.Connected = api.Connected;
                Api = api;
            }
            catch (ServerException sex) { serverMsg = sex.Message; }
        }
        #endregion

        #region Events(MT5):
        ConnectProgress connectStatus;
        public bool IsLoggedIn => connectStatus == ConnectProgress.AcceptAuthorized;
        void Api_OnConnectProgress(MT5API sender, ConnectEventArgs args)
        {
            bool loggedIn = connectStatus == ConnectProgress.AcceptAuthorized;
            connectStatus = args.Progress;
            if (args.Progress == ConnectProgress.AcceptAuthorized)
            {
               // if (!loggedIn) LoggedIn?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (loggedIn)
                {
               //     LoggedOut?.Invoke(this, EventArgs.Empty);
                    symbolToTick.Clear();
                }
            }
        }

        void Api_OnQuote(MT5API api, Quote args)
        {
            Bid = args.Bid;
            Ask = args.Ask;
            Time = args.Time;
            //try
            //{
            //    Balance = api.Account.Balance;
            //}
            //catch { }

            //TickEventArgs tick = null;

            //lock (symbolToTick)
            //{
            //    if (symbolToTick.ContainsKey(args.Symbol))
            //        tick = symbolToTick[args.Symbol];
            //}

            //if (tick != null)
            //{
            //    tick.Bid = args.Bid;
            //    tick.Ask = args.Ask;
            //    tick.BrokerTime = args.Time;
            //    Time = args.Time;
            //    Tick?.Invoke(this, tick);
            //}
        }

        void Logger_OnMsg(object sender, string msg, Logger.MsgType type)
        {
            LogMsg = $"{msg} | {type}";
        }
        #endregion

        #region Methods:
        public void Subscribe(string symbol)
        {
            lock (symbolToTick)
            {
                try
                {
                    if (api != null)
                    {
                        if (!symbolToTick.ContainsKey(symbol))
                        {
                            TickEventArgs tick = new TickEventArgs();
                            tick.Symbol = symbol;
                            symbolToTick[symbol] = tick;
                            api.Subscribe(symbol);
                        }
                    }
                }
                catch (Exception e)
                {
                    //logger.LogError(ViewId + " " + e.Message);
                }
            }
        }
        public void Start(ulong login, string password, string address, int port)
        {
            //Stop();
            if (api == null)
            {
                try
                {
                    api = new MT5API(login, password, address, port);
                    api.ConnectTimeout = 10000;
                    api.OnConnectProgress += Api_OnConnectProgress;
                    api.OnQuote += Api_OnQuote;
                    api.Connect();
                }
                catch (Exception e)
                {
                    //logger.LogError(ViewId + " " + e.Message);
                }
            }
            Logger.OnMsg += Logger_OnMsg;
        }

        public void Stop()
        {
            if (api != null)
            {
                try
                {
                    api.OnConnectProgress -= Api_OnConnectProgress;
                    api.OnQuote -= Api_OnQuote;
                    api.Disconnect();
                }
                catch (Exception e)
                {
                    //logger.LogError(ViewId + " " + e.Message);
                }
                api = null;
            }
        }

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
