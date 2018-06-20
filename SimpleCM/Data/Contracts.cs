using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCM.Data
{
    class Contracts: ObservableCollection<Contract>
    {
        public Contracts()
        {
            Add(new Contract("扬州金域蓝湾1期"));
            Add(new Contract("南京朗诗玲珑屿2期"));
            Add(new Contract("苏州保利樾公馆2期"));
            Add(new Contract("扬州恒大帝京"));
            Add(new Contract("扬州恒大帝京"));
        }
    }
}
