using SimpleCM.Data;
using SimpleCM.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<string> AddtionList = new ObservableCollection<string>(); 
        public EditWindow()
        {
            InitializeComponent();
            DetailEdit.SetEditMode();
            addtion_list.ItemsSource = AddtionList;
        }

        private void OnChangePath(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fd = new System.Windows.Forms.OpenFileDialog();
            //System.Windows.Interop.HwndSource source = PresentationSource.FromVisual(this) as System.Windows.Interop.HwndSource;
            System.Windows.Forms.DialogResult result = fd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                AddtionList.Add(fd.FileName);
            }
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
                Log.D("", "项目名和项目编号都不能为空");
                return;
            }
            contract.IsReadOnly = true;
            contract.BillNoteInfo = billNote.Tostring();
            string addInfo = "";
            if (AddtionList.Count > 0)
            {
                addInfo = string.Join(" ", AddtionList);
            }
            contract.Aditionals = addInfo;
            Contracts.Instance.Add(contract);
            ContractDB.Instance.Insert(contract);
            this.Close();
        }

        private void OnDelPath(object sender, RoutedEventArgs e)
        {
            AddtionList.Remove((string)addtion_list.SelectedItem);
        }
    }
}
