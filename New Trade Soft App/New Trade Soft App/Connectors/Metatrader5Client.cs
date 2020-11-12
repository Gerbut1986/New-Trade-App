namespace New_Trade_Soft_App.Connectors
{
    using System;
    using mtapi.mt5;
    using System.Collections.Generic;

    internal class Metatrader5Client : IConnector
    {
        string username;
        string password;
        string server;

        class ClientInfo
        {
            public MT5API api;
        }

        ClientInfo client;
        Dictionary<string, TickEventArgs> symbolToTick;
        ConnectProgress connectStatus;
        IConnectorLogger logger;
        public string ViewId => "MT5 " + username;
        public FillPolicy Fill { get; set; }
        public decimal Balance { get; protected set; }

        public Metatrader5Client(IConnectorLogger logger, string username, string password, string server)
        {
            this.logger = logger;
            this.username = username;
            this.password = password;
            this.server = server;
            symbolToTick = new Dictionary<string, TickEventArgs>();
            connectStatus = ConnectProgress.Disconnect;
            client = new ClientInfo();
        }

        public event EventHandler LoggedIn;
        public event EventHandler<TickEventArgs> Tick;
        public event EventHandler LoggedOut;

        public void Start()
        {
            Stop();
            if (client.api == null)
            {
                try
                {
                    string[] h = server.Split(new char[] { ':' });
                    int host = int.Parse(h[1]);
                    client.api = new MT5API(ulong.Parse(username), h[0], password, host);
                    client.api.ConnectTimeout = 10000;
                    client.api.OnConnectProgress += Api_OnConnectProgress;
                    client.api.OnQuote += Api_OnQuote;
                    client.api.Connect();
                }
                catch (Exception e)
                {
                    logger.LogError(ViewId + " " + e.Message);
                }
            }
        }

        void Api_OnConnectProgress(MT5API sender, ConnectEventArgs args)
        {
            bool loggedIn = connectStatus == ConnectProgress.AcceptAuthorized;
            connectStatus = args.Progress;
            if (args.Progress == ConnectProgress.AcceptAuthorized)
            {
                if (!loggedIn) LoggedIn?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (loggedIn)
                {
                    LoggedOut?.Invoke(this, EventArgs.Empty);
                    symbolToTick.Clear();
                }
            }
        }

        public void Stop()
        {
            if (client.api != null)
            {
                try
                {
                    client.api.OnConnectProgress -= Api_OnConnectProgress;
                    client.api.OnQuote -= Api_OnQuote;
                    client.api.Disconnect();
                }
                catch (Exception e)
                {
                    logger.LogError(ViewId + " " + e.Message);
                }
                client.api = null;
            }
        }

        public bool IsLoggedIn => connectStatus == ConnectProgress.AcceptAuthorized;

        public DateTime CurrentTime { get; private set; }

        void Api_OnQuote(MT5API api, Quote args)
        {
            try
            {
                Balance = (decimal)api.Account.Balance;
            }
            catch { }

            TickEventArgs tick = null;

            lock (symbolToTick)
            {
                if (symbolToTick.ContainsKey(args.Symbol))
                    tick = symbolToTick[args.Symbol];
            }

            if (tick != null)
            {
                tick.Bid = (decimal)args.Bid;
                tick.Ask = (decimal)args.Ask;
                tick.BrokerTime = args.Time;
                CurrentTime = args.Time;
                Tick?.Invoke(this, tick);
            }
        }

        public void Subscribe(string symbol)
        {
            lock (symbolToTick)
            {
                try
                {
                    if (client.api != null)
                    {
                        if (!symbolToTick.ContainsKey(symbol))
                        {
                            TickEventArgs tick = new TickEventArgs();
                            tick.Symbol = symbol;
                            symbolToTick[symbol] = tick;
                            client.api.Subscribe(symbol);
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(ViewId + " " + e.Message);
                }
            }
        }

        public void Unsubscribe(string symbol)
        {
            lock (symbolToTick)
            {
                try
                {
                    if (client.api != null)
                    {
                        if (symbolToTick.ContainsKey(symbol))
                        {
                            symbolToTick.Remove(symbol);
                            client.api.Unsubscribe(symbol);
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(ViewId + " " + e.Message);
                }
            }
        }
    }
}
