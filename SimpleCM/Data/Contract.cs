using SimpleCM.Tools;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SimpleCM.Data
{
    public class Contract: INotifyPropertyChanged
    {
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
        string format = "项目编号:{0}\n项目名称:{1}\n项目类别:{2}\n项目日期:{3}\n签约单位:{4}\n合作单位:{5}\n项目描述:{6}";
        
        public string BaseInfo
        {
            get
            {
                DateTime datetime = Util.GetTimeFromMillis(ContractDate);
                string dateInfo = string.Format("{0}年{1}月{2}日", datetime.Year, datetime.Month, datetime.Day);
                return string.Format(format, ContractNumber, ContractName, ProjectCategory,
                    dateInfo, ContractCompany, CooperatorCompany, ProjectDescription);
            }
            set
            {
                _baseInfo = value;
                OnPropertyChanged("BaseInfo");
            }
        }
        public string Undefined { set; get; }

        private bool _isReadOnly = false;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
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


        public override string ToString() => ContractName;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));

        }
    }
}
