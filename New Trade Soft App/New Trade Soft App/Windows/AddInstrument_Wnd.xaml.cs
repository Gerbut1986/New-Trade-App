namespace New_Trade_Soft_App.Windows
{
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Collections.Generic;

    public partial class AddInstrument_Wnd : Window
    {
        string[] symbols;
        string str = "";

        public AddInstrument_Wnd()
        {
            InitializeComponent();
            using (StreamReader sr = new StreamReader("Symbols.txt"))
                str = sr.ReadToEnd();
            symbols = str.Split(new char[] { '\n', '\r' }).ToArray();
            List<string> list = symbols.ToList();
            list.RemoveAll(empty => empty == "");
            foreach (string s in list)
                symb_cmb.Items.Add(s);
            int[] scnds = new int[60];
            for (int i = 1; i < scnds.Length; i++) scnds[i] = i;
            foreach (int sec in scnds)
                quotesIntrv_txt.Items.Add(sec);
            attemt_lbl.Visibility = Visibility.Hidden;
        }

        void sbmt_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Symbol = symb_cmb.Text; 
            try
            {
                MainWindow.Lot = int.Parse(lot_txt.Text == "" ? 0.ToString() : lot_txt.Text);
                MainWindow.Seconds = int.Parse(quotesIntrv_txt.Text);
            }
            catch { MainWindow.Lot = MainWindow.Seconds = 0; }
                if (MainWindow.Symbol != null)
            {
                this.Close();
            }
        }

        void Image_MouseLeave(object sender, MouseEventArgs e) =>
            attemt_lbl.Visibility = Visibility.Hidden;

        void Image_MouseEnter(object sender, MouseEventArgs e) =>
            attemt_lbl.Visibility = Visibility.Visible;

        void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        void close_btn_Click(object sender, RoutedEventArgs e) => Close();

        void lot_txt_GotFocus(object sender, RoutedEventArgs e) => lot_txt.Text = "";
    }
}
