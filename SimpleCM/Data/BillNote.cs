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
            string format = "项目名称:{0}\n公司名称:{1}\n纳税人识别号:{2}\n地址:{3}\n电话:{4}\n银行:{5}\n账号:{6}";
            return string.Format(format, ProjectName, CompanyName, TaxFileNum,
                Address, PhoneNumber, Bank, BankAccount);
        }

        public BillNote FromString(string str)
        {
            
            string[] noteItems = str.Split('\n');
            if (noteItems != null && noteItems.Length == 7)
            {
                ProjectName = noteItems[0].Substring(noteItems[0].IndexOf(':') + 1);
                CompanyName = noteItems[1].Substring(noteItems[1].IndexOf(':') + 1);
                TaxFileNum = noteItems[2].Substring(noteItems[2].IndexOf(':') + 1);
                Address = noteItems[3].Substring(noteItems[3].IndexOf(':') + 1);
                PhoneNumber = noteItems[4].Substring(noteItems[4].IndexOf(':') + 1);
                Bank = noteItems[5].Substring(noteItems[5].IndexOf(':') + 1);
                BankAccount = noteItems[6].Substring(noteItems[6].IndexOf(':') + 1);
            }
            return this;
        }
    }
}
