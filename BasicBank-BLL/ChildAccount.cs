using System;
using System.IO;


public class ChildAccount : Account
{
  private string parentName;
  public ChildAccount(string parentName, string ownerName, string accountType, int age = 0, string address = "", int balance = 0)
      : base(ownerName, accountType, age, address, balance)
  {
    this.parentName = parentName;
  }
  public ChildAccount(TextReader textIn)
      : base(textIn)
  {
    this.parentName = textIn.ReadLine();
  }
  public bool SetParentName(string newName)
  {
    if (!ValidateNameOrAddress(newName))
    {
      return false;
    }
    this.parentName = newName.Trim();
    return true;
  }
  public override int WithdrawMoney(int amountToWithdraw)
  {
    if (amountToWithdraw > 1000)
    {
      Console.WriteLine("\n\nThis is a child account. You cannot withdraw more than 1000 pounds at a single transaction!");
      return 0;
    }
    return base.WithdrawMoney(amountToWithdraw);
  }
  public override string GetAccountInfo()
  {
    return "Parent Name: " + this.parentName + "\n"
        + base.GetAccountInfo();
  }
  public override bool Save(TextWriter textOut)
  {
    if (base.Save(textOut))
    {
      textOut.WriteLine(this.parentName);
      return true;
    };
    return false;
  }
}