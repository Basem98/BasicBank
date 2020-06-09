namespace BasikBank_BLL
{
  class AccountFactory
  {
    public static IAccount CreateAccount(string accountType, System.IO.TextReader textIn)
    {
      switch (accountType)
      {
        case "Account":
          {
            return new Account(textIn);
          }
        case "ChildAccount":
          {
            return new ChildAccount(textIn);
          }
        default:
          {
            return null;
          }
      }
    }
  }
}