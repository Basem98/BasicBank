using System;
using System.IO;


public class Account : IAccount
{
  private string ownerName;
  private int ownerAge;
  private string ownerAddress;
  private Guid accountNumber;
  private int accountBalance;
  private AccountState accountState;
  private AccountType accountType;
  public Account(string ownerName, string accountType, int age, string address, int initialBalance)
  {
    this.ownerName = ownerName;
    this.ownerAge = age;
    this.ownerAddress = address;
    this.accountBalance = initialBalance;
    this.accountNumber = Guid.NewGuid();
    this.accountState = AccountState.New;
    this.accountType = accountType == "Current" ? AccountType.Current : AccountType.Savings;
  }
  public Account(TextReader textIn)
  {
    this.ownerName = textIn.ReadLine();
    this.ownerAge = int.Parse(textIn.ReadLine());
    this.ownerAddress = textIn.ReadLine();
    Enum.TryParse(textIn.ReadLine(), out this.accountState);
    this.accountNumber = Guid.Parse(textIn.ReadLine());
    this.accountType = textIn.ReadLine() == "Current" ? AccountType.Current : AccountType.Savings;
    this.accountBalance = int.Parse(textIn.ReadLine());
  }
  public bool SetOwnerName(string newName)
  {
    if (!ValidateNameOrAddress(newName))
    {
      return false;
    }
    this.ownerName = newName.Trim();
    return true;
  }
  public string GetOwnerName()
  {
    return this.ownerName;
  }
  public bool SetAge(int newAge)
  {
    if (newAge < 16 || newAge > 99)
    {
      return false;
    }
    this.ownerAge = newAge;
    return true;
  }
  public int GetAge()
  {
    return this.ownerAge;
  }
  public bool SetAddress(string newAddress)
  {
    if (!ValidateNameOrAddress(newAddress))
    {
      return false;
    }
    this.ownerAddress = newAddress.Trim();
    return true;
  }
  public string GetAddress()
  {
    return this.ownerAddress;
  }
  public string GetAccountType()
  {
    return this.accountType.ToString();
  }
  public bool SetAccountType(string accountType)
  {
    this.accountType = accountType == "Current" ? AccountType.Current : AccountType.Savings;
    return true;
  }
  public static bool ValidateNameOrAddress(string toBeValidated)
  {
    if (toBeValidated.Length == 0 || toBeValidated.Trim().Length == 0)
    {
      return false;
    }
    return true;
  }
  public bool MakeDeposit(int amountToDeposit)
  {
    if (amountToDeposit > 0 && amountToDeposit < 10000)
    {
      this.accountBalance += amountToDeposit;
      return true;
    }
    return false;
  }
  public virtual int WithdrawMoney(int amountToWithdraw)
  {
    if (amountToWithdraw > 10000 || amountToWithdraw > this.accountBalance)
    {
      Console.WriteLine("\n\nThis is an adult account. You cannot withdraw more than 10000 pounds at a single transaction!");
      return 0;
    }
    this.accountBalance -= amountToWithdraw;
    return amountToWithdraw;

  }
  public int PrintStatement()
  {
    return this.accountBalance;
  }

  public virtual string GetAccountInfo()
  {
    return "Owner Name: " + this.ownerName + "\n"
        + "Owner Age: " + this.ownerAge.ToString() + "\n"
        + "Owner Address: " + this.ownerAddress + "\n"
        + "Account Number: " + this.accountNumber.ToString() + "\n"
        + "Account Type: " + this.accountType.ToString() + "\n"
        + "Account State: " + this.accountState.ToString();
  }

  public bool CloseAccount()
  {
    this.accountState = AccountState.Closed;
    return true;
  }
  public bool ActivateAccount()
  {
    this.accountState = AccountState.Activated;
    return true;
  }
  public virtual bool Save(TextWriter textOut)
  {
    Console.WriteLine("Inside the Save method that takes a stream writer as a parameter");
    textOut.WriteLine(this.ownerName);
    textOut.WriteLine(this.ownerAge);
    textOut.WriteLine(this.ownerAddress);
    textOut.WriteLine(this.accountState);
    textOut.WriteLine(this.accountNumber);
    textOut.WriteLine(this.accountType);
    textOut.WriteLine(this.accountBalance);
    return true;
  }
}