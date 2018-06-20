using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCM.Data
{
    class BillNote
    {
        public string ProjectName { set; get; }
        public string CompanyName { set; get; }
        public string TaxFileNum { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        public string Bank { set; get; }
        public string BankAccount { set; get; }
        public string Undefined { set; get; }
    }
}
