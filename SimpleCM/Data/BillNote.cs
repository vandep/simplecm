namespace SimpleCM.Data
{
    public class BillNote
    {
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
            string format = "公司名称:{0}\n纳税人识别号:{1}\n地址:{2}\n电话:{3}\n银行:{4}\n账号:{5}";
            return string.Format(format, CompanyName, TaxFileNum,
                Address, PhoneNumber, Bank, BankAccount);
        }

        public BillNote FromString(string str)
        {
            
            string[] noteItems = str.Split('\n');
            if (noteItems != null && noteItems.Length == 6)
            {
                CompanyName = noteItems[0].Substring(noteItems[0].IndexOf(':') + 1);
                TaxFileNum = noteItems[1].Substring(noteItems[1].IndexOf(':') + 1);
                Address = noteItems[2].Substring(noteItems[2].IndexOf(':') + 1);
                PhoneNumber = noteItems[3].Substring(noteItems[3].IndexOf(':') + 1);
                Bank = noteItems[4].Substring(noteItems[4].IndexOf(':') + 1);
                BankAccount = noteItems[5].Substring(noteItems[5].IndexOf(':') + 1);
            }
            return this;
        }
    }
}
