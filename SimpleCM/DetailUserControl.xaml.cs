using SimpleCM.Data;
using SimpleCM.Tools;
using System;
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

        //public void SetEditMode()
        //{
        //    date_edit.Visibility = System.Windows.Visibility.Visible;
        //    tip_name_eidt.Visibility = System.Windows.Visibility.Visible;
        //    tip_num_eidt.Visibility = System.Windows.Visibility.Visible;
        //}

        public Contract GetContractFromText()
        {
            Contract contract = null;
            if (!string.IsNullOrEmpty(contract_name_edit.Text) && !string.IsNullOrEmpty(contract_num_edit.Text))
            {
                contract = new Contract();
                contract.ContractName = contract_name_edit.Text;
                contract.ContractNumber = contract_num_edit.Text;
                contract.CooperatorCompany = cooperator_company_edit.Text;
                contract.ContractCompany = contract_company_edit.Text;
                if (!string.IsNullOrEmpty(contract_date_edit.Text))
                {
                    DateTime dateTime = Convert.ToDateTime(contract_date_edit.Text);
                    contract.ContractDate = Util.GetTimeMillis(dateTime.Year, dateTime.Month, dateTime.Day);
                }

                if (!string.IsNullOrEmpty(cost_edit.Text))
                {
                    contract.Cost = long.Parse(cost_edit.Text);
                }
                if (!string.IsNullOrEmpty(peceivables_1_edit.Text))
                {
                    contract.Peceivables_1 = long.Parse(peceivables_1_edit.Text);
                }

                if (!string.IsNullOrEmpty(peceivables_2_edit.Text))
                {
                    contract.Peceivables_2 = long.Parse(peceivables_2_edit.Text);
                }
                if (!string.IsNullOrEmpty(peceivables_3_edit.Text))
                {
                    contract.Peceivables_3 = long.Parse(peceivables_3_edit.Text);
                }                
                contract.ProjectDescription = description_edit.Text;
                contract.ProjectCategory = project_cat_edit.Text;
            }
            return contract;
        }
    }
}
