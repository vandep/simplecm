using SimpleCM.Data;
using SimpleCM.Tools;
using System.Collections.ObjectModel;
using System.Windows;

namespace SimpleCM
{
    /// <summary>
    /// EditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditWindow : Window
    {
        public ObservableCollection<string> AddtionList = new ObservableCollection<string>();
        private bool isEdit = false;
        private Contract mContract = null;
        public EditWindow()
        {
            InitializeComponent();
            //DetailEdit.SetEditMode();

            addtion_list.ItemsSource = AddtionList;
        }

        public void SetData(Contract c)
        {
            isEdit = true;
            mContract = c;
            DataContext = c; ;
            if (c.AddtionList != null && c.AddtionList.Count > 0)
            {
                foreach (string a in c.AddtionList)
                {
                    AddtionList.Add(a);
                }
            }
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


            Contract contract = DetailEdit.GetContractFromText();
            if (contract == null)
            {
                Log.D("", "项目名和项目编号都不能为空");
                return;
            }
            contract.BillNoteCompanyName = bill_company_name.Text;
            contract.BillNoteTaxFileNum = bill_tax_number.Text;
            contract.BillNoteAddress = bill_address.Text;
            contract.BillNoteBank = bill_bank_name.Text;
            contract.BillNoteBankAccount = bill_bank_acount.Text;
            contract.BillNotePhoneNumber = bill_phone.Text;
            contract.BillNoteInfo = contract.BillNoteTostring();
            string addInfo = "";
            if (AddtionList.Count > 0)
            {
                addInfo = string.Join(" ", AddtionList);
            }
            contract.Aditionals = addInfo;
            if (!isEdit)
            {
                Contracts.Instance.Add(contract);
                ContractDB.Instance.InsertItem(contract);
            }
            else
            {
                //更新
                if (contract.ContractDate <= 0)
                {
                    contract.ContractDate = mContract.ContractDate;
                }
                int index = Contracts.Instance.IndexOf(mContract);
                Contracts.Instance.Remove(mContract);
                Contracts.Instance.Insert(index, contract);
                ContractDB.Instance.UpdateItem(mContract.Id, contract);
            }

            this.Close();
        }

        private void OnDelPath(object sender, RoutedEventArgs e)
        {
            AddtionList.Remove((string)addtion_list.SelectedItem);
        }
    }
}
