using SimpleCM.Tools;
using System.Collections.ObjectModel;

namespace SimpleCM.Data
{
    class Contracts: ObservableCollection<Contract>
    {
        public static Contracts Instance { get; } = new Contracts();
        public void AddContact(Contract c)
        {
            Add(c);
            ContractDB.Instance.Insert(c);
        }
    }
}
