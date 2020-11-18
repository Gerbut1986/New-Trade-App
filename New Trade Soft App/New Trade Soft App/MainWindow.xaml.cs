namespace New_Trade_Soft_App
{
    using System;
    using mtapi.mt5;
    using System.IO;
    using System.Windows;
    using System.Threading;
    using System.Windows.Threading;
    using New_Trade_Soft_App.Models;
    using New_Trade_Soft_App.Windows;
    using System.Collections.Generic;

    public partial class MainWindow : Window
    {
        bool isConnect;
        public static MT5API api;
        public static Quote quote;
        object sync = new object();
        int rowIndxInput, rowIndxConn;
        public static int Lot { get; set; }
        public static int Seconds { get; set; }
        public static string Symbol { get; set; }
        public static string Address { get; set; }
        ManualResetEvent threadStop = new ManualResetEvent(false);
        ManualResetEvent threadStopped = new ManualResetEvent(true);

        public MainWindow()
        {
            InitializeComponent();
            stop_btn.Visibility = del_btn.Visibility = Visibility.Hidden;
            addConn_btn.IsEnabled = false;
            //servers_cmb.IsEditable = user_lbl.IsEnabled = user_txt.IsEnabled = pass_lbl.IsEnabled =
            //    pass_txt.IsEnabled = addConn_btn.IsEnabled = disconect_btn.IsEnabled = false;
        }

        #region Event(Window):
        void addConn_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (servers_cmb.Text.Equals(""))
                    Title = "First you need to select any symbol from combo box!";
                else
                {
                    Start(null);
                    isConnect = MT5Model.Api.Connected;
                    userEmail_lbl.Content = "Trader Name: ";
                    mail_lbl.Content = MT5Model.Api.Account.UserName;
                    Title = MT5Model.Api.Account.Email;
                    stop_btn.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex) { Title = ex.Message; isConnect = false; }
            finally
            {
                //user_txt.Text = string.Empty;
                //pass_txt.Text = string.Empty;
            }
        }

        void stop_btn_Click(object sender, RoutedEventArgs e) => Stop();

        void del_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Project.Inputs[rowIndxInput].api == null)
            {
                Model.Project.Inputs.Remove(inputGrid.SelectedValue as MT5Model);
                del_btn.Visibility = Visibility.Hidden;
                return;
            }
            else if (Model.Project.Inputs[rowIndxInput].api.Connected)
            {
                Title = "Can NOT to Delete row! Because already connected...";
                del_btn.Visibility = Visibility.Hidden;
                return;
            }
            else
            {
                Model.Project.Inputs.Remove(inputGrid.SelectedValue as MT5Model);
                del_btn.Visibility = Visibility.Hidden;
            }
        }

        void disconect_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Project.ConnectInfo[rowIndxConn].Connected)
            {
                Model.Project.ConnectInfo[rowIndxConn].Connected = false;
                Model.Project.ConnectInfo.Remove(userGrid.SelectedValue as ConnectionModel);
                return;
            }
            else
            {
                Model.Project.ConnectInfo.Remove(userGrid.SelectedValue as ConnectionModel);
                disconect_btn.Visibility = Visibility.Hidden;
            }


            ConnectionModel selected = userGrid.SelectedItem as ConnectionModel;
            selected.Connected = false;
            if (api != null) api.Disconnect();
            else return;
        }

        void inputGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            del_btn.Visibility = Visibility.Visible;
            rowIndxInput = inputGrid.SelectedIndex;
        }

        void userGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            disconect_btn.Visibility = Visibility.Visible;
            rowIndxConn = userGrid.SelectedIndex;
        }

        void setInst_btn_Click(object sender, RoutedEventArgs e)
        {
            new AddInstrument_Wnd().ShowDialog();
            if (Symbol == null || Symbol == "") return;
            else
            {
                Model.Project.Inputs.Add(new MT5Model(Symbol, Seconds));
                Symbol = "";
                addConn_btn.IsEnabled = true;
            }
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string ip in ConnectionModel.ServersDat())
                servers_cmb.Items.Add(ip);
            date_lbl.Content = $"{DateTime.Now.Day}  {Date.GetMonths[DateTime.Now.Month]} {DateTime.Now.Year}";
            disconect_btn.Visibility = Visibility.Hidden;
            LoadProject(new ProjectModel());
        }

        public static event Action OnClossing;
        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OnClossing?.Invoke();
            Stop();
        }
        #endregion

        #region Auxiliary methods:
        void Start(string p)
        {
            Model.Project.Started = true;
            sync = new object();
            threadStop.Reset();
            threadStopped.Reset();

            ConnectionModel conn_info = new ConnectionModel
            {
                Address = servers_cmb.Text,
                Username = user_txt.Text,
                Password = pass_txt.Text,
            };

            if (Model.Project.ConnectInfo.Count == 0)
                Model.Project.ConnectInfo.Add(conn_info);
            else
            {
                for (int i = 0; i < Model.Project.ConnectInfo.Count; i++)
                {
                    if (user_txt.Text == Model.Project.ConnectInfo[i].Username)
                        break;
                    else
                        Model.Project.ConnectInfo.Add(conn_info);
                }
            }

            foreach (MT5Model input in Model.Project.Inputs)
            {
                if (input.GetType() == typeof(MT5Model))
                    Title = input.Connect_MT5(conn_info);

                input.FlowAsk = 0;
                input.FlowAsk = 0;
                input.PrevAsk = 0;
                input.PrevBid = 0;
                input.PrevLast = 0;
                input.GapBuy = 0;
                input.GapSell = 0;
                input.Lot = 0;
            }

            new Thread(thread).Start(p);
        }

        void Stop()
        {
            if (!Model.Project.Started) return;
            threadStop.Set();
            while (!threadStopped.WaitOne(1000))
                DoEvents();
            foreach (var item in Model.Project.Inputs)
                if (item.GetType() == typeof(MT5Model))
                    item.Stop();
        }

        public static void DoEvents() =>
           Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));

        public void LoadProject(ProjectModel project)
        {
            Model.Project = project;
            DataContext = Model.Project;
        }
        #endregion

        #region Thread method:
        class Local_Input : IProcessorInput
        {
            public MT5Model model;
            public double Ask { get; set; }
            public double Bid { get; set; }
            public DateTime Time { get; set; }
        }

        void thread(object pparam)
        {
            string inputfile = pparam as string;
            List<Local_Input> inputs = new List<Local_Input>();
            for (int i = 0; i < Model.Project.Inputs.Count; i++)
            {
                inputs.Add(new Local_Input()
                {
                    model = Model.Project.Inputs[i],
                });
            }

            FileStream fi = null;
            if (inputfile != null)
                if (File.Exists(inputfile))
                    fi = new FileStream(inputfile, FileMode.Open);

            DateTime fiTime = DateTime.MinValue;
            while (!threadStop.WaitOne(0))
            {
                bool changed = false;
                bool valid = true;
                if (fi == null)
                {
                    lock (sync)
                    {
                        for (int i = 0; i < inputs.Count; i++)
                        {
                            changed = changed || inputs[i].model.CopyToPrev();
                            valid = valid && inputs[i].model.IsValid();
                            inputs[i].Bid = inputs[i].model.Bid;
                            inputs[i].Ask = inputs[i].model.Ask;
                            inputs[i].Time = inputs[i].model.Time;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < inputs.Count; i++)
                    {
                        inputs[i].Bid = readDouble(fi);
                        inputs[i].Ask = readDouble(fi);
                        inputs[i].Time = new DateTime(readLong(fi));
                    }
                    long ticks = readLong(fi);
                    if (ticks == 0) break;
                    fiTime = new DateTime(ticks);
                    changed = true;
                }
            }

            threadStopped.Set();
        }

        byte[] tmp = new byte[8];
        double readDouble(FileStream fs)
        {
            int readed = fs.Read(tmp, 0, 8);
            if (readed == 8)
                return BitConverter.ToDouble(tmp, 0);
            return 0.0;
        }

        long readLong(FileStream fs)
        {
            int readed = fs.Read(tmp, 0, 8);
            if (readed == 8)
                return BitConverter.ToInt64(tmp, 0);
            return 0;
        }
        #endregion
    }
}
