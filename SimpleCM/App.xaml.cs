using SimpleCM.Data;
using System.Threading;
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
        }
    }
}
