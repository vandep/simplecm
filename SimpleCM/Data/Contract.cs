using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCM.Data
{
    class Contract: INotifyPropertyChanged
    {
        private string _contractNumber;
        public string ContractNumber
        {
            set
            {
                _contractNumber = value;
                OnPropertyChanged("ContractNumber");
            }

            get
            {
                return _contractNumber;
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
            get
            {
                return _contractName;
            }
        }
        public long ContractDate { set; get; }
        public string ProjectDescription { set; get; }
        public string ContractCompany { set; get; }
        public string CooperatorCompany { set; get; }
        public double Cost { set; get; }
        public double[] Receivables { set; get; }
        public string Undefined { set; get; }

        public Contract(string name)
        {
            _contractName = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;



        public override string ToString() => ContractName;



        protected void OnPropertyChanged(string info)

        {

            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(info));

        }
    }
}
