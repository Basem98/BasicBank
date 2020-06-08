//These are the interface that the program's components are going to implement
public interface IBankSystem
{
    bool StoreAccount(IAccount newAccount);
    bool hasAnAccount(string ownerName);
    IAccount GetAccount(string ownerName);
    int GetNumberOfAccounts();
    bool Save(string filename);
    System.Collections.Generic.Dictionary<string, IAccount> GetAllAccounts();
}
public interface IAccount
{
    int PrintStatement();
    int WithdrawMoney(int amountToWithdraw);
    bool MakeDeposit(int amountToDeposit);
    string GetOwnerName();
    bool SetOwnerName(string newName);
    int GetAge();
    bool SetAge(int newAge);
    string GetAddress();
    bool SetAddress(string newAddress);
    string GetAccountType();
    bool SetAccountType(string accountType);
    string GetAccountInfo();
    bool CloseAccount();
    bool ActivateAccount();
    bool Save(System.IO.TextWriter textOut);
}
public interface IBankUI
{
    void GetAccountReference(int commandNumber);
    void CallCorrespondingMethod(int commandNumber);
    void InitiateBankProgram();
    void ExitOrReturn();
    void GetAgeRange(out string accountAgeRange);
    void GetOwnerName(out string ownerName);
    void GetAccountType(out string accoutType);
    void GetAge(out int age, in string accountAgeRange);
    void GetAddress(out string address);
    void makeInitialDeposit(out int initialBalance);
    void CreateNewAccount();
    void EditExistingAccount();
    void EditName();
    void EditAge();
    void EditAddress();
    void ChangeAccountType();
    void ShowAccountInfo();
    void PrintStatement();
    void CloseAccount();
    void ActivateAccount();
    void MakeDeposit();
    void WithdrawMoney();
    void SaveBeforeClosing(string filename);

}