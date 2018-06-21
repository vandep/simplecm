using SimpleCM.Data;
using SimpleCM.Tools;
using System;
using System.Windows;

namespace SimpleCM
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditWindow editWindow = new EditWindow
                {
                    Owner = this
                };
                editWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                Log.E("", "show add contract window error:" + ex.Message);
            }
        }

        private void Delete_Contract(object sender, RoutedEventArgs e)
        {

        }
    }
}
