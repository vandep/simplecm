using SimpleCM.Tools;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SimpleCM.Data
{
    public class Contract: INotifyPropertyChanged
    {
        public long Id { get; set; }
        private string _contractNumber;

        public string ContractNumber
        {
            set
            {
                _contractNumber = value;
                OnPropertyChanged("ContractNumber");
            }

            get => _contractNumber;
        }
        private string _projectCategory;
        public string ProjectCategory
        {
            get => _projectCategory;
            set
            {
                _projectCategory = value;
                OnPropertyChanged("ProjectCategory");
            }
        }

        private string _contractName;
        public string ContractName
        {
            set
            {
                _contractName = value;
                OnPropertyChanged("ContractName");
            }
            get => _contractName;
        }

        private long _contractDate;
        public long ContractDate
        {
            set
            {
                _contractDate = value;
                OnPropertyChanged("ContractDate");
            }
            get => _contractDate;
        }

        private string _projectDescription;
        public string ProjectDescription
        {
            set
            {
                _projectDescription = value;
                OnPropertyChanged("ProjectDescription");
            }
            get => _projectDescription;
        }

        private string _contractCompany;
        public string ContractCompany
        {
            set
            {
                _contractCompany = value;
                OnPropertyChanged("ContractCompany");
            }
            get => _contractCompany;
        }

        private string _cooperatorCompany;
        public string CooperatorCompany
        {
            set
            {
                _cooperatorCompany = value;
                OnPropertyChanged("CooperatorCompany");
            }
            get => _cooperatorCompany;
        }

        private long _cost;
        public long Cost
        {
            set
            {
                _cost = value;
                OnPropertyChanged("Cost");
            }
            get => _cost;
        }

        private long _peceivables_1;
        public long Peceivables_1
        {
            get => _peceivables_1;
            set
            {
                _peceivables_1 = value;
                OnPropertyChanged("Peceivables_1");
            }
        }

        private long _peceivables_2;
        public long Peceivables_2
        {
            get => _peceivables_2;
            set
            {
                _peceivables_2 = value;
                OnPropertyChanged("Peceivables_2");
            }
        }

        private long _peceivables_3;
        public long Peceivables_3
        {
            get => _peceivables_3;
            set
            {
                _peceivables_3 = value;
                OnPropertyChanged("Peceivables_3");
            }
        }

        private string _baseInfo;
        readonly string format = "项目编号:{0}\n项目名称:{1}\n项目类别:{2}\n项目日期:{3}\n" +
            "签约单位:{4}\n合作单位:{5}\n项目描述:{6}\n总费用:{7}\n" +
            "应付款项一:{8}\n应付款项二:{9}\n应付款项三:{10}"; 
        
        public string BaseInfo
        {
            get => _baseInfo;
            set
            {
                _baseInfo = value;
                OnPropertyChanged("BaseInfo");
            }
        }

        public string BaseInfoTostring()
        {
            DateTime datetime = Util.GetTimeFromMillis(ContractDate);
            string dateInfo = string.Format("{0}年{1}月{2}日", datetime.Year, datetime.Month, datetime.Day);
            return string.Format(format, ContractNumber, ContractName, ProjectCategory,
                dateInfo, ContractCompany, CooperatorCompany, ProjectDescription, Cost, Peceivables_1, Peceivables_2, Peceivables_3);
        }

        public void FromBaseInfo()
        {
            string[] infos = _baseInfo.Split('\n');
            if (infos != null && infos.Length == 11)
            {
                //ContractNumber = infos[0].Substring(infos[0].IndexOf(':') + 1);
                //ContractName = infos[1].Substring(infos[1].IndexOf(':') + 1);
                //ProjectCategory = infos[2].Substring(infos[2].IndexOf(':') + 1);
                ContractCompany = infos[4].Substring(infos[4].IndexOf(':') + 1);
                CooperatorCompany = infos[5].Substring(infos[5].IndexOf(':') + 1);
                ProjectDescription = infos[6].Substring(infos[6].IndexOf(':') + 1);
                Cost = long.Parse(infos[7].Substring(infos[7].IndexOf(':') + 1));
                Peceivables_1 = long.Parse(infos[8].Substring(infos[8].IndexOf(':') + 1));
                Peceivables_2= long.Parse(infos[9].Substring(infos[9].IndexOf(':') + 1));
                Peceivables_3 = long.Parse(infos[10].Substring(infos[10].IndexOf(':') + 1));
            }
        }

        private string _aditionals;
        public string Aditionals
        {
            get => _aditionals;
            set
            {
                _aditionals = value;
                OnPropertyChanged("Aditionals");
            }
        }

        public ObservableCollection<string> AddtionList
        {
            get
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                if (!string.IsNullOrEmpty(_aditionals))
                {
                    string[] infos = _aditionals.Split(' ');
                    if (infos != null && infos.Length > 0)
                    {
                        for (int i = 0; i < infos.Length; i++)
                        {
                            list.Add(infos[i]);
                        }
                    }
                }
                return list;
            }
        }


        public string BillNoteTostring()
        {
            //不能加@,否则原样输出\r\n,不换行
            string format = "公司名称:{0}\n纳税人识别号:{1}\n地址:{2}\n电话:{3}\n银行:{4}\n账号:{5}";
            return string.Format(format, BillNoteCompanyName, BillNoteTaxFileNum,
                BillNoteAddress, BillNotePhoneNumber, BillNoteBank, BillNoteBankAccount);
        }

        public void FromBillNoteString()
        {

            string[] noteItems = _billNoteInfo.Split('\n');
            if (noteItems != null && noteItems.Length == 6)
            {
                BillNoteCompanyName = noteItems[0].Substring(noteItems[0].IndexOf(':') + 1);
                BillNoteTaxFileNum = noteItems[1].Substring(noteItems[1].IndexOf(':') + 1);
                BillNoteAddress = noteItems[2].Substring(noteItems[2].IndexOf(':') + 1);
                BillNotePhoneNumber = noteItems[3].Substring(noteItems[3].IndexOf(':') + 1);
                BillNoteBank = noteItems[4].Substring(noteItems[4].IndexOf(':') + 1);
                BillNoteBankAccount = noteItems[5].Substring(noteItems[5].IndexOf(':') + 1);
            }
        }

        private string _billNoteInfo;
        public string BillNoteInfo
        {
            get => _billNoteInfo;
            set
            {
                _billNoteInfo = value;
                OnPropertyChanged("BillNoteInfo");
            }
        }

        private string _billNoteCompanyName;
        public string BillNoteCompanyName
        {
            get
            {
                return _billNoteCompanyName;
            }
            set
            {
                _billNoteCompanyName = value;
                OnPropertyChanged("BillNoteCompanyName");
            }
        }
        private string _billNoteTaxFileNum;
        public string BillNoteTaxFileNum
        {
            get
            {
                return _billNoteTaxFileNum;
            }
            set
            {
                _billNoteTaxFileNum = value;
                OnPropertyChanged("BillNoteTaxFileNum");
            }
        }

        private string _billNoteAddress;
        public string BillNoteAddress
        {
            get
            {
                return _billNoteAddress;
            }
            set
            {
                _billNoteAddress = value;
                OnPropertyChanged("BillNoteAddress");
            }
        }

        private string _billNotePhoneNumber;
        public string BillNotePhoneNumber
        {
            get
            {
                return _billNotePhoneNumber;
            }
            set
            {
                _billNotePhoneNumber = value;
                OnPropertyChanged("BillNotePhoneNumber");
            }
        }

        private string _billNoteBank;
        public string BillNoteBank
        {
            get
            {
                return _billNoteBank;
            }
            set
            {
                _billNoteBank = value;
                OnPropertyChanged("BillNoteBank");
            }
        }

        private string _billNoteBankAccount;
        public string BillNoteBankAccount
        {
            get
            {
                return _billNoteBankAccount;
            }
            set
            {
                _billNoteBankAccount = value;
                OnPropertyChanged("BillNoteBankAccount");
            }
        }



        public override string ToString() => ContractName;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));

        }
    }
}
