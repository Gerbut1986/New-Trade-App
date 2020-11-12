namespace New_Trade_Soft_App.Windows
{
    using System.Windows;
    using New_Trade_Soft_App.Models;

    public partial class ServersDAT_Wnd : Window
    {
        public ServersDAT_Wnd()
        {
            InitializeComponent();
            foreach (var s in ConnectionModel.ServersDat())
                listServers.Items.Add(s);
        }
    }
}
