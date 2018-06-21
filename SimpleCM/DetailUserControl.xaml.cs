using SimpleCM.Data;
using System.Windows.Controls;

namespace SimpleCM
{
    /// <summary>
    /// DetailUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class DetailUserControl : UserControl
    {
        public DetailUserControl()
        {
            InitializeComponent();
        }

        public Contract GetContractFromText()
        {
            Contract contract = null;
            if (!string.IsNullOrEmpty(contract_name_edit.Text))
            {
                contract = new Contract();
                contract.ContractName = contract_name_edit.Text;
                contract.ContractNumber = contract_num_edit.Text;
                contract.CooperatorCompany = cooperator_company_edit.Text;
                contract.ContractCompany = contract_company_edit.Text;
                contract.ContractDate = long.Parse(contract_date_edit.Text);
                contract.Cost = long.Parse(cost_edit.Text);
                contract.Peceivables_1 = long.Parse(peceivables_1_edit.Text);
                contract.Peceivables_2 = long.Parse(peceivables_2_edit.Text);
                contract.Peceivables_3 = long.Parse(peceivables_3_edit.Text);
                contract.ProjectDescription = description_edit.Text;
            }
            return contract;

        }
    }
}
