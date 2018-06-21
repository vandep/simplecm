using SimpleCM.Tools;
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
        private static readonly Contracts instance = new Contracts();

        public static Contracts Instance
        {
            get { return instance; }
        }

        private Contracts()
        {
            Contract contract = new Contract("扬州金域蓝湾1期")
            {
                ContractDate = Util.GetTimeMillis(2017, 12, 1),
                Cost = 150000,
                ContractCompany = "博石绿建科技",
                CooperatorCompany = "江苏省设计院",
                Peceivables_1 = 50000,
                Peceivables_2 = 50000,
                Peceivables_3 = 50000,
                ProjectDescription = @"中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在
                        津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学vvvvvvv中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学中核集团将在津投资建设中国核工业大学"
            };
            Add(contract);

            Add(new Contract("南京朗诗玲珑屿2期"));
            Add(new Contract("苏州保利樾公馆2期"));
            Add(new Contract("扬州大学体育场"));
            Add(new Contract("苏州保利樾公馆2期"));
            Add(new Contract("扬州恒大帝京"));
            Add(new Contract("苏州保利樾公馆2期"));
            Add(new Contract("苏州保利樾公馆2期"));
            Add(new Contract("扬州恒大帝京"));
            Add(new Contract("苏州保利樾公馆2期"));
            Add(new Contract("扬州恒大帝京"));
            Add(new Contract("苏州保利樾公馆2期"));
            Add(new Contract("扬州恒大帝京"));
            Add(new Contract("苏州保利樾公馆2期"));
            Add(new Contract("扬州金域蓝湾1期"));
            Add(new Contract("扬州恒大帝京"));
            Add(new Contract("扬州金域蓝湾1期"));
            Add(new Contract("扬州恒大帝京"));
            
        }

        public void AddContact()
        {
            Add(new Contract("扬州金域蓝湾2期"));
        }
    }
}
