namespace New_Trade_Soft_App.Connectors
{
    using System;

    class TickEventArgs : EventArgs
    {
        public string Symbol { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public DateTime BrokerTime { get; set; }
    }

    interface IConnectorLogger
    {
        void LogClear();
        void LogInfo(string msg);
        void LogError(string msg);
        void LogWarning(string msg);
    }

    interface IConnector
    {
        event EventHandler LoggedIn;
        event EventHandler<TickEventArgs> Tick;
        event EventHandler LoggedOut;

        void Start();
        void Stop();
        bool IsLoggedIn { get; }
        DateTime CurrentTime { get; }
        string ViewId { get; }
        decimal Balance { get; }
        void Subscribe(string symbol);
        void Unsubscribe(string symbol);
    }
}