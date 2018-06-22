using SimpleCM.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleCM
{
    /// <summary>
    /// EditPasswordUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class EditPasswordWindow : Window
    {
        public EditPasswordWindow()
        {
            InitializeComponent();
        }

        private void Ok_btn_Click(object sender, RoutedEventArgs e)
        {
            string pre = pre_pwd.Password;
            if (string.IsNullOrEmpty(pre) || !pre.Equals(IniFile.Instance.GetPwd()))
            {
                error_tips.Text = "原密码输入错误！";
                error_tips.Visibility = Visibility.Visible;
                return;
            }
            string newPwd = new_pwd.Password;
            string confirm = new_pwd_confirm.Password;
            if (string.IsNullOrEmpty(newPwd) || string.IsNullOrEmpty(newPwd) || !newPwd.Equals(confirm))
            {
                error_tips.Text = "两次密码不一致！";
                error_tips.Visibility = Visibility.Visible;
                return;
            }
            IniFile.Instance.SetPwd(newPwd);
            Close();
        }

        private void Pwd_MouseEnter(object sender, MouseEventArgs e)
        {
            error_tips.Visibility = Visibility.Collapsed;
        }
    }
}
