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
            //不能加@,否则原样输出\r\n,不换行
            string format = "项目名称:{0}\r\n公司名称:{1}\r\n纳税人识别号:{2}\r\n地址:{3}\r\n电话:{4}\r\n银行:{5}\r\n账号:{6}";
            return string.Format(format, ProjectName, CompanyName, TaxFileNum,
                Address, PhoneNumber, Bank, BankAccount);
        }
    }
}
