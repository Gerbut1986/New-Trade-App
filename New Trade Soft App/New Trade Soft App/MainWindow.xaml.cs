namespace New_Trade_Soft_App
{
    using System;
    using mtapi.mt5;
    using System.Windows;
    using New_Trade_Soft_App.Models;
    using New_Trade_Soft_App.Windows;
    using System.Collections.Generic;

    public partial class MainWindow : Window
    {
        int host;
        public static MT5API api;
        public static Quote quote;
        bool isConnect;
        List<string> symbols = new List<string>();
        public static int Lot { get; set; }
        public static string Symbol { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Events(MT5API):
        void Api_OnQuote(MT5API api, Quote quote)
        {
            Model.Project.Inputs[0].Ask = quote.Ask;
            Model.Project.Inputs[0].Bid = quote.Bid;
            Model.Project.Inputs[0].Time = quote.Time;
            user_txt.Text = quote.Ask.ToString();
        }

        void Api_OnConnectProgress(MT5API sender, ConnectEventArgs args)
        {

        }
        #endregion

        #region Event(Window):
        void addConn_btn_Click(object sender, RoutedEventArgs e)
        {
            string[] h = servers_cmb.Text.Split(new char[] { ':' });
            if (h.Length == 1)
            {
                Title = "This is not an address to connect to MT5...Try select again!";
                return;
            }
            else
            {
                servers_cmb.Text = h[0];
                host = int.Parse(h[1]);
            }
            api = new MT5API(ulong.Parse(user_txt.Text), pass_txt.Text, servers_cmb.Text, host);
            api.OnConnectProgress += Api_OnConnectProgress;
            api.OnQuote += Api_OnQuote;

            try
            {
                api.Connect();
                isConnect = api.Connected;
                userEmail_lbl.Content = "Trader Name: ";
                Dictionary<string, SymGroup> smbls = api.Symbols.Groups;
                Dictionary<string, SymGroup>.KeyCollection keys = smbls.Keys;
                foreach (string key in keys) symbols.Add(key);
                mail_lbl.Content = api.Account.UserName;
                AddInstrument_Wnd a = new AddInstrument_Wnd(symbols);
                a.ShowDialog();
                Title = api.Account.Email;
                api.Subscribe(Symbol);
                quote = api.GetQuote(Symbol);
                //while (api.GetQuote(Symbol) == null)
                //    System.Threading.Thread.Sleep(1);
                Connections conn_info = new Connections
                {
                    Address = servers_cmb.Text,
                    Username = user_txt.Text,
                    Password = pass_txt.Text,
                    Connected = isConnect
                };
                Model.Project.ConnectInfo.Add(conn_info);
            }
            catch(Exception ex) { Title = ex.Message; isConnect = false; }
            finally
            {
                user_txt.Text = string.Empty;
                pass_txt.Text = string.Empty;
            }
        }

        void disconect_btn_Click(object sender, RoutedEventArgs e)
        {
            Connections selected = userGrid.SelectedItem as Connections;
            selected.Connected = false;
            if (api != null) api.Disconnect();
        }

        void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            disconect_btn.Visibility = Visibility.Visible;
        }

        void setInst_btn_Click(object sender, RoutedEventArgs e)
        {
            AddInstrument_Wnd a = new AddInstrument_Wnd(symbols);
            a.ShowDialog();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string ip in Connections.ServersDat())
                servers_cmb.Items.Add(ip);
            date_lbl.Content = $"Update:   {DateTime.Now.Day}    {Date.GetMonths[DateTime.Now.Month]}  {DateTime.Now.Year}";
            disconect_btn.Visibility = Visibility.Hidden;
            LoadProject(new ProjectModel());
        }
        #endregion

        #region Auxiliary methods:
        public void LoadProject(ProjectModel project)
        {
            Model.Project = project;
            DataContext = Model.Project;
        }

        #endregion

    }
}
