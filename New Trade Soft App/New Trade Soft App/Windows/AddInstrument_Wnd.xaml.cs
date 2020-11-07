namespace New_Trade_Soft_App.Windows
{
    using mtapi.mt5;
    using System.Windows;
    using System.Windows.Input;
    using System.Collections.Generic;

    public partial class AddInstrument_Wnd : Window
    {
        List<string> symbols;

        public AddInstrument_Wnd(List<string> symbols)
        {
            InitializeComponent();
            this.symbols = symbols;
            foreach (string s in symbols)
                symb_cmb.Items.Add(s);
            attemt_lbl.Visibility = Visibility.Hidden;
        }

        void sbmt_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Symbol = symb_cmb.Text;
            try
            {
                MainWindow.Lot = int.Parse(lot_txt.Text);
            }
            catch { MainWindow.Lot = 0; }
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
