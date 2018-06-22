using SimpleCM.Data;
using SimpleCM.Tools;
using System.Windows;

namespace SimpleCM
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            ContractDB.Instance.CreateTable();
            ContractDB.Instance.LoadAll();
            IniFile.CreateInstance();
            if (string.IsNullOrEmpty(IniFile.Instance.GetPwd()))
            {
                IniFile.Instance.SetPwd("123456");
            }

        }
    }
}
