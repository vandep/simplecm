using SimpleCM.Tools;
using System.Windows;

namespace SimpleCM
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Ok_btn_Click(object sender, RoutedEventArgs e)
        {
            string tb = pwd_input.Password;
            if (!string.IsNullOrEmpty(tb) && tb.Equals(IniFile.Instance.GetPwd()))
            {
                try
                {
                    MainWindow mainWindow = new MainWindow();
                    Close();
                    mainWindow.ShowDialog();
                }
                catch
                {

                }
            }
            else
            {
                pwd_error_tip.Visibility = Visibility.Visible;
            }
        }

        private void Pwd_input_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            pwd_error_tip.Visibility = Visibility.Collapsed;
            pwd_input.Password = "";
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditPasswordWindow editWindow = new EditPasswordWindow();
                editWindow.ShowDialog();
            }
            catch
            {

            }
        }
    }
}
