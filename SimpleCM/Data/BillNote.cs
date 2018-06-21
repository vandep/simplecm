namespace SimpleCM.Data
{
    public class BillNote
    {
        public string ProjectName { set; get; }
        public string CompanyName { set; get; }
        public string TaxFileNum { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        public string Bank { set; get; }
        public string BankAccount { set; get; }
        public string Undefined { set; get; }
        public string Tostring()
        {
            string format = @"项目名称:{0}\n公司名称:{1}\n纳税人识别号:{2}\n地址:{3}\n电话:{4}\n银行:{5}\n账号{6}";
            return string.Format(format, ProjectName, CompanyName, TaxFileNum,
                Address, PhoneNumber, Bank, BankAccount);
        }
    }
}
