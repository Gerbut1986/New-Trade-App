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
        DateTime time;
        int port, delay, lot;
        string symbol, logMsg;
        ConnectionModel connectionModel;
        public event EventHandler<TickEventArgs> Tick;
        static Dictionary<string, TickEventArgs> symbolToTick;
        double ask, bid, last, prevBid, prevAsk, prevLast, balance, _GapBuy, _GapSell;
        #endregion

        #region Constructors:
        public MT5Model()
        {
            Time = DateTime.MinValue;
            symbolToTick = new Dictionary<string, TickEventArgs>();
        }

        public MT5Model(string symbol, int delay)
        {
            Symbol = symbol;
            this.delay = delay;
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

        public int Delay
        {
            get { return delay; }
            set { if (delay != value) { delay = value; OnPropertyChanged(); } }
        }

        public int Lot
        {
            get { return lot; }
            set { if (lot != value) { lot = value; OnPropertyChanged(); } }
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
        public double GapBuy
        {
            get { return _GapBuy; }
            set { if (_GapBuy != value) { _GapBuy = value; OnPropertyChanged(); } }
        }

        [XmlIgnore]
        public double GapSell
        {
            get { return _GapSell; }
            set { if (_GapSell != value) { _GapSell = value; OnPropertyChanged(); } }
        }

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
        public MT5API api;
        public static MT5API Api { get; set; }
        public string serverMsg;

        public string Connect_MT5(ConnectionModel model)
        {
            int cnt = 1;
            string address, msg_res = "";
            connectionModel = model;
            string[] h = model.Address.Split(new char[] { ':' });
            if (h.Length == 1 && !model.Connected) return "";
            else
            {
                address = h[0];
                port = int.Parse(h[1]);
            }
            if (model.Connected)
            {
                Start(ulong.Parse(model.Username), model.Password, address, port);
                Subscribe(symbol);
                //while (api.GetQuote(symbol) == null)
                //{
                //   SMTP.SendEmail(api.Account.Email, api.Account.UserName, $"Delay starts for the '{delay}' seconds!!!");
                //}        
            }

            Start(ulong.Parse(model.Username), model.Password, address, port);

            try
            {
                Subscribe(symbol);
                //while (api.GetQuote(symbol) == null)
                //{
                //    cnt++;
                //    if (cnt == delay)
                //        SMTP.SendEmail(api.Account.Email, api.Account.UserName, $"Delay starts for the '{delay}' seconds!!!");
                //}

                model.Connected = api.Connected;
                Api = api;
            }
            catch (ServerException sex) { serverMsg = sex.Message; }
            return msg_res;
        }
        #endregion

        #region Events(MT5):
        public static ConnectProgress connectStatus;
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

        public void Api_OnQuote(MT5API api, Quote args)
        {
            //if(connectStatus.ToString() == "Disconnect" || connectStatus.ToString() == "Exception")
            //    Start(ulong.Parse(connectionModel.Username), connectionModel.Password, connectionModel.Address, port);
            System.Threading.Thread.Sleep(delay);
            if(delay != 0)
                SMTP.SendEmail(api.Account.Email, api.Account.UserName,
                    $"Fin Instrument - '{args.Symbol}'\nDelay starts for the '{delay}' seconds!!!");
            Bid = args.Bid;
            Ask = args.Ask;
            Time = args.Time;
        }

        void Logger_OnMsg(object sender, string msg, Logger.MsgType type)
        {
            LogMsg = $"{msg} | {type} | {connectStatus}";
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
            try
            {
                api = new MT5API(login, password, address, port);
                //api.ConnectTimeout = 10000;
                api.OnConnectProgress += Api_OnConnectProgress;
                api.OnQuote += Api_OnQuote;
                api.Connect();
            }
            catch (Exception e)
            {
                //logger.LogError(ViewId + " " + e.Message);
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
