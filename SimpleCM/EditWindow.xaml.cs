using SimpleCM.Data;
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
using System.Windows.Shapes;

namespace SimpleCM
{
    /// <summary>
    /// EditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }

        private void Submit_btn_Click(object sender, RoutedEventArgs e)
        {
            BillNote billNote = new BillNote();
            billNote.CompanyName = bill_company_name.Text;
            billNote.TaxFileNum = bill_tax_number.Text;
            billNote.Address = bill_address.Text;
            billNote.Bank = bill_bank_name.Text;
            billNote.BankAccount = bill_bank_acount.Text;
            billNote.PhoneNumber = bill_phone.Text;

            Contract contract = DetailEdit.GetContractFromText();
            if (contract == null)
            {
                Log.D("", "项目名不能为空");
                return;
            }
            contract.BillNoteInfo = billNote.Tostring();
            Contracts.Instance.Add(contract);
            ContractDB.Instance.Insert(contract);
            this.Close();
        }
    }
}
