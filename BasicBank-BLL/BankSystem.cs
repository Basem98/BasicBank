using System.Collections.Generic;
using System.IO;

namespace BasikBank_BLL
{
    public class BankSystem : IBankSystem
    {
        private Dictionary<string, IAccount> bankDatabase;
        private int numberOfAccounts;
        public BankSystem()
        {
            this.bankDatabase = new Dictionary<string, IAccount>();
            this.numberOfAccounts = 0;
        }
        public bool hasAnAccount(string ownerName)
        {
            return this.bankDatabase.ContainsKey(ownerName.Trim());
        }
        public bool StoreAccount(IAccount newAccount)
        {
            this.bankDatabase.Add(newAccount.GetOwnerName(), newAccount);
            this.numberOfAccounts += 1;
            return true;
        }
        public IAccount GetAccount(string ownerName)
        {
            return this.bankDatabase[ownerName];
        }
        public int GetNumberOfAccounts()
        {
            return this.numberOfAccounts;
        }
        public Dictionary<string, IAccount> GetAllAccounts()
        {
            return this.bankDatabase;
        }
        public bool Save(string filename)
        {
            TextWriter textOut = new StreamWriter(filename);
            textOut.WriteLine(this.bankDatabase.Count);
            foreach (IAccount account in this.bankDatabase.Values)
            {
                textOut.WriteLine(account.GetType().Name);
                if (!account.Save(textOut))
                {
                    return false;
                };
            }
            textOut.Close();
            return true;
        }
        public static IBankSystem Load(string filename)
        {
            TextReader textIn = new StreamReader(filename);
            BankSystem result = new BankSystem();
            string countString = textIn.ReadLine();
            if (countString != null)
            {
                result.numberOfAccounts = int.Parse(countString);
                for (int i = 0; i < result.numberOfAccounts; i++)
                {
                    string accountType = textIn.ReadLine();
                    IAccount account = AccountFactory.CreateAccount(accountType, textIn);
                    result.bankDatabase.Add(account.GetOwnerName(), account);
                }
            }
            textIn.Close();
            return result;
        }
    }
}
