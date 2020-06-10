using System;
using BasikBank_BLL;

namespace BasikBank_ConsoleUI
{
    public static class BasikBank_ConsoleUI
    {
        struct NewAccountInformation
        {
            public string accountAgeRange;
            public string ownerName;
            public string parentName;
            public string accountType;
            public int age;
            public string address;
            public int initialBalance;
        }

        class BankUI : IBankUI
        {
            private IBankSystem bankSystem;
            private IAccount account;
            public BankUI(IBankSystem bankSystem)
            {
                this.bankSystem = bankSystem;
                this.account = null;
            }
            //A method that asks the user to enter his full name before giving them access to the account
            public void GetAccountReference(int commandNumber)
            {
                string ownerName = "";
                if (commandNumber == 1 || this.account != null)
                {
                    return;
                }

                do
                {
                    Console.Clear();
                    Console.WriteLine("Please enter your accurate account's owner name to have access to the required action!\n\n");
                    Console.Write("Owner Name: ");
                    ownerName = Console.ReadLine();
                } while (!Account.ValidateNameOrAddress(ownerName));

                while (!Account.ValidateNameOrAddress(ownerName) || !this.bankSystem.hasAnAccount(ownerName))
                {
                    Console.WriteLine("\n\nThere is no account in our database associated with the name you've entered!");
                    Console.WriteLine("Please make sure that the name you have entered is accurate and that it's the name you opened your account with\n\n");
                    Console.Write("Owner Name: ");
                    ownerName = Console.ReadLine();
                }

                this.account = this.bankSystem.GetAccount(ownerName);
            }
            public void CallCorrespondingMethod(int commandNumber)
            {
                this.GetAccountReference(commandNumber);

                switch (commandNumber)
                {
                    case 1:
                        {
                            this.CreateNewAccount();
                            return;
                        }
                    case 2:
                        {
                            this.EditExistingAccount();
                            return;
                        }
                    case 3:
                        {
                            this.ShowAccountInfo();
                            return;
                        }
                    case 4:
                        {
                            this.PrintStatement();
                            return;
                        }
                    case 5:
                        {
                            this.CloseAccount();
                            return;
                        }
                    case 6:
                        {
                            this.ActivateAccount();
                            return;
                        }
                    case 7:
                        {
                            this.MakeDeposit();
                            return;
                        }
                    case 8:
                        {
                            this.WithdrawMoney();
                            return;
                        }
                    default:
                        return;
                }
            }
            public void InitiateBankProgram()
            {
                Console.Clear();
                int commandNumber = 0;
                do
                {
                    try
                    {
                        Console.WriteLine("Welcome to BasicBank's program!");
                        Console.WriteLine("Please enter the number of the action you would like to take from the list below:\n");
                        Console.WriteLine("1. Open a new account in BasicBank");
                        Console.WriteLine("2. Edit an existing account");
                        Console.WriteLine("3. Show your existing account's information");
                        Console.WriteLine("4. Get a bank statement with the current balance");
                        Console.WriteLine("5. Close your account in BasicBank");
                        Console.WriteLine("6. Activate a closed account");
                        Console.WriteLine("7. Make a deposit into your account");
                        Console.WriteLine("8. Withdraw money from your account");
                        Console.WriteLine("\n\nEnter your command:");
                        commandNumber = int.Parse(Console.ReadLine());
                    }
                    catch (Exception exception)
                    {
                        Console.Clear();
                        Console.WriteLine(exception.GetType().Name + ": " + exception.Message + "\n\n");
                        continue;
                    }
                } while ((commandNumber < 1 || commandNumber > 8));
                this.CallCorrespondingMethod(commandNumber);
                this.SaveBeforeClosing("../../../BasicBank-BLL/bin/debug/Bank System.txt");
                return;
            }
            public void ExitOrReturn()
            {
                int commandNumber = 3;
                Console.WriteLine("\n\nEnter 0 to go back to the main menu or 1 to exit the program");
                Console.Write("Enter your command: ");
                try
                {
                    commandNumber = int.Parse(Console.ReadLine());
                }
                catch (Exception exception)
                {
                    Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                    Console.WriteLine("You have to choose either 1 or 0, or you could click on the X sign above to close the program instead!");
                    this.ExitOrReturn();
                }
                switch (commandNumber)
                {
                    case 0:
                        {
                            this.InitiateBankProgram();
                            return;
                        }
                    case 1:
                        {
                            return;
                        }
                    default:
                        {
                            break;
                        }
                }
                return;
            }
            //Check whether the new user wants a child or an adult account
            public void GetAgeRange(out string accountAgeRange)
            {
                int commandNumber = 0;
                do
                {
                    try
                    {
                        Console.WriteLine("\n\nYou can only enter either 1 or 2 to choose between the two types of accounts we provide at the moment");
                        Console.WriteLine("Please choose the type of the account you want to open:\n\n");
                        Console.WriteLine("1. Child Account");
                        Console.WriteLine("2. Adult Account");
                        Console.Write("\nEnter your choice: ");
                        commandNumber = int.Parse(Console.ReadLine());
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                        Console.WriteLine("Please choose a number between the range of numbers provided in the menu!");
                        continue;
                    }
                }
                while (commandNumber < 1 || commandNumber > 2);

                accountAgeRange = commandNumber == 1 ? "ChildAccount" : "Account";
                return;
            }
            public void GetOwnerName(out string ownerName)
            {
                do
                {
                    Console.WriteLine("\n\nEnter your valid full name");
                    Console.Write("\nOwner Full Name: ");
                    ownerName = Console.ReadLine();
                } while (!Account.ValidateNameOrAddress(ownerName));

                return;
            }
            public void GetAccountType(out string accountType)
            {
                int commandNumber = 0;
                do
                {
                    try
                    {
                        Console.WriteLine("\n\nDo you want to open a Current or a Savings account?");
                        Console.WriteLine("1. Current Account");
                        Console.WriteLine("2. Savings Account");
                        Console.Write("\nAccount Type: ");
                        commandNumber = int.Parse(Console.ReadLine());
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                        Console.WriteLine("Please choose a number between the range of numbers provided in the menu!");
                        continue;
                    }
                } while (commandNumber < 1 || commandNumber > 2);

                accountType = commandNumber == 1 ? "Current" : "Savings";
                return;
            }
            public void GetAge(out int age, in string accountAgeRange)
            {
                do
                {
                    try
                    {
                        Console.WriteLine("\n\nPlease enter your accurate age");
                        Console.Write("\nAge: ");
                        age = int.Parse(Console.ReadLine());

                        //Prohibit users above the age of 16 of creating a child's account
                        if (accountAgeRange == "ChildAccount" && age > 16)
                        {
                            Console.WriteLine("\n\nNote that the child account is only available for children between the age of 10 and 16");
                            age = 0;
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                        age = 0;
                        Console.WriteLine("Please write an integer in the range allowed for your account type!");
                        continue;
                    }
                } while (age < 10);

                return;
            }
            public void GetAddress(out string address)
            {
                do
                {
                    Console.WriteLine("\n\nPlease enter your current address");
                    Console.Write("\nCurrent Address: ");
                    address = Console.ReadLine();
                } while (!Account.ValidateNameOrAddress(address));

                return;
            }
            public void makeInitialDeposit(out int initialBalance)
            {
                try
                {
                    Console.WriteLine("\n\nDo you want to make a deposit with your initial balance? If so, add it below");
                    Console.WriteLine("If you don't want, you can just write 0");
                    Console.Write("\nInitial Balance: ");
                    initialBalance = int.Parse(Console.ReadLine());
                }
                catch (Exception exception)
                {
                    Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                    Console.WriteLine("\nYou have to enter any integer starting from 0 to represent the initial deposit you want to make!");
                    this.makeInitialDeposit(out initialBalance);
                }
                return;
            }
            public void CreateNewAccount()
            {
                NewAccountInformation newAccountInfo;

                Console.Clear();
                Console.WriteLine("Great Choice! We offer accounts for adults or for children above the age of 10 and below 16\n");

                this.GetAgeRange(out newAccountInfo.accountAgeRange);
                this.GetOwnerName(out newAccountInfo.ownerName);
                this.GetAccountType(out newAccountInfo.accountType);
                this.GetAge(out newAccountInfo.age, in newAccountInfo.accountAgeRange);
                this.GetAddress(out newAccountInfo.address);
                this.makeInitialDeposit(out newAccountInfo.initialBalance);

                if (newAccountInfo.accountAgeRange == "ChildAccount")
                {
                    do
                    {
                        Console.WriteLine("\n\nIf you're interested in opening a child account, then you have to provide the full name of the child's parent");
                        Console.Write("\nParent Full Name: ");
                        newAccountInfo.parentName = Console.ReadLine();
                    } while (!Account.ValidateNameOrAddress(newAccountInfo.parentName));

                    this.account = new ChildAccount(newAccountInfo.parentName, newAccountInfo.ownerName, newAccountInfo.accountType, newAccountInfo.age, newAccountInfo.address, newAccountInfo.initialBalance);
                }

                this.account = new Account(newAccountInfo.ownerName, newAccountInfo.accountType, newAccountInfo.age, newAccountInfo.address, newAccountInfo.initialBalance);

                bankSystem.StoreAccount(this.account);
                bankSystem.Save("../../../BasicBank-BLL/bin/debug/Bank System.txt");

                if (this.account != null && bankSystem.GetAccount(newAccountInfo.ownerName).Equals(this.account))
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations {0}! Your account with the basic bank has been created successfully!", this.account.GetOwnerName());
                    Console.WriteLine("\nThis is your account information:\n\n");
                    Console.WriteLine(this.account.GetAccountInfo());
                    this.ExitOrReturn();
                    return;
                }
            }
            public void EditExistingAccount()
            {
                int commandNumber = -1;
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome back, {0}! What do you want to edit in your account's information?\n", this.account.GetOwnerName());
                        Console.WriteLine("0. Go back to the main menu");
                        Console.WriteLine("1. Edit your name");
                        Console.WriteLine("2. Update your age");
                        Console.WriteLine("3. Change your address");
                        Console.WriteLine("4. Change your account type");
                        Console.WriteLine("5. Exit");
                        Console.Write("\nEnter your command: ");
                        commandNumber = int.Parse(Console.ReadLine());
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                        Console.WriteLine("Please only write down a number between the range of numbers provided in the menu!");
                        continue;
                    }
                } while (commandNumber < 0 || commandNumber > 5);

                switch (commandNumber)
                {
                    case 0:
                        {
                            this.InitiateBankProgram();
                            return;
                        }
                    case 1:
                        {
                            this.EditName();
                            return;
                        }
                    case 2:
                        {
                            this.EditAge();
                            return;
                        }
                    case 3:
                        {
                            this.EditAddress();
                            return;
                        }
                    case 4:
                        {
                            this.ChangeAccountType();
                            return;
                        }
                    case 5:
                        {
                            return;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            public void EditName()
            {
                string newOwnerName;
                do
                {
                    Console.Clear();
                    Console.WriteLine("The name currently stored in our bank's database is: {0}", this.account.GetOwnerName());
                    Console.Write("Enter your new full name: ");
                    newOwnerName = Console.ReadLine();
                } while (!Account.ValidateNameOrAddress(newOwnerName));

                if (this.account.SetOwnerName(newOwnerName))
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations! The name associated with your account is now: {0}", this.account.GetOwnerName());
                    this.ExitOrReturn();
                    return;
                }
                return;
            }
            public void EditAge()
            {
                int newAge = 0;
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Your current age is: {0}", this.account.GetAge());
                        Console.Write("Enter your new age: ");
                        newAge = int.Parse(Console.ReadLine());
                        if (newAge < 16)
                        {
                            Console.WriteLine("If your age is below 16, you should open a child account instead!");
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                        newAge = 0;
                        Console.WriteLine("Please write an integer in the range allowed for your account type to represent your accurate age!");
                        continue;
                    }
                } while (newAge < 16 && newAge > 99);
                if (this.account.SetAge(newAge))
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations! The age stored into our database is now: {0}", this.account.GetAge());
                    this.ExitOrReturn();
                    return;
                }
                return;
            }
            public void EditAddress()
            {
                string newAddress;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Your current registered address is: {0}", this.account.GetAddress());
                    Console.Write("Enter your new address: ");
                    newAddress = Console.ReadLine();
                } while (!Account.ValidateNameOrAddress(newAddress));

                if (this.account.SetAddress(newAddress))
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations! Your new address has been stored into our database, which is: {0}", this.account.GetAddress());
                    this.ExitOrReturn();
                    return;
                }
                return;
            }
            public void ChangeAccountType()
            {
                string newAccountType;
                int commandNumber = 0;
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Your current account is a {0} account\n", this.account.GetAccountType());
                        Console.WriteLine("Do you want to change it to:");
                        Console.WriteLine("1. Current Account");
                        Console.WriteLine("2. Savings Account");
                        Console.Write("\nEnter either 1 (Current) or 2 (Savings): ");
                        commandNumber = int.Parse(Console.ReadLine());
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                        Console.WriteLine("Please choose a number between the range of numbers provided in the menu!");
                        continue;
                    }
                } while (commandNumber < 1 || commandNumber > 2);

                newAccountType = commandNumber == 1 ? "Current" : "Savings";
                if (this.account.SetAccountType(newAccountType))
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations! Your account is now a {0} account", this.account.GetAccountType());
                    this.ExitOrReturn();
                    return;
                }
                return;
            }
            public void ShowAccountInfo()
            {
                Console.Clear();
                Console.WriteLine("Hello, {0}! Below is all the information associated with your account in the BasicBank's database\n", this.account.GetOwnerName());
                Console.WriteLine(this.account.GetAccountInfo());
                this.ExitOrReturn();
                return;
            }
            public void PrintStatement()
            {
                Console.Clear();
                Console.WriteLine("Welcome back, {0}", this.account.GetOwnerName());
                Console.WriteLine("Your current balance is: {0}", this.account.PrintStatement());
                this.ExitOrReturn();
                return;
            }
            public void CloseAccount()
            {
                int commandNumber = 0;
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("We're sorry that it came to this!");
                        Console.WriteLine("But you can still reactivate your account again without going through the hassle of reopening a new account with us\n");
                        Console.WriteLine("1. Close my account");
                        Console.WriteLine("2. Don't close my account");
                        Console.Write("\n\nEnter your command: ");
                        commandNumber = int.Parse(Console.ReadLine());
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                        Console.WriteLine("Please choose a number between the range of numbers provided in the menu!");
                        continue;
                    }
                } while (commandNumber < 1 || commandNumber > 2);

                Console.Clear();
                switch (commandNumber)
                {
                    case 1:
                        {
                            if (this.account.CloseAccount())
                            {
                                Console.WriteLine("Your account with the BasicBank has been closed till a further notice!");
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Your account with the BasicBank is still activated and functional!");
                            break;
                        }
                    default: break;
                }

                this.ExitOrReturn();
                return;
            }

            public void ActivateAccount()
            {
                Console.Clear();
                Console.WriteLine("We're thrilled that you have changed your mind, {0}!", this.account.GetOwnerName());

                if (this.account.ActivateAccount())
                {
                    Console.WriteLine("\nYour account has now been activated again!");
                }

                this.ExitOrReturn();
                return;
            }
            public void MakeDeposit()
            {
                int balanceBeforeNewDeposit = this.account.PrintStatement();
                int amountToDeposit = 0;

                Console.Clear();
                do
                {
                    try
                    {
                        Console.WriteLine("Hello, {0}", this.account.GetOwnerName());
                        Console.WriteLine("Your current balance is: {0}", this.account.PrintStatement());
                        Console.WriteLine("\nPlease take into your consideration that you CANNOT make a single deposit of a value more than 10000 pounds!\n");
                        Console.Write("Enter the amount of money you want to deposit here: ");
                        amountToDeposit = int.Parse(Console.ReadLine());

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                        Console.WriteLine("\nYou have to enter any integer starting from 1 to 10000 to represent the amount of money you want to deposit!\n\n");
                        continue;
                    }
                } while (!this.account.MakeDeposit(amountToDeposit));

                if (this.account.PrintStatement() == (balanceBeforeNewDeposit + amountToDeposit))
                {
                    Console.Clear();
                    Console.WriteLine("The transaction has been completed successfully!");
                    Console.WriteLine("Your current balance is now: {0}", this.account.PrintStatement());
                }
                this.ExitOrReturn();
                return;
            }
            public void WithdrawMoney()
            {
                int balanceBeforeWithdraw = this.account.PrintStatement();
                int amountToWithdraw = 0;

                Console.Clear();
                do
                {
                    try
                    {
                        Console.WriteLine("Hello, {0}", this.account.GetOwnerName());
                        Console.WriteLine("Your current balance is: {0}", this.account.PrintStatement());
                        Console.Write("\n\nEnter the amount of money you want to withdraw here: ");
                        amountToWithdraw = int.Parse(Console.ReadLine());
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                        Console.WriteLine("\nYou have to enter any integer starting from 1 to 10000 to represent the amount of money you want to withdraw!");
                        continue;
                    }
                } while (amountToWithdraw == 0);

                if (amountToWithdraw > balanceBeforeWithdraw || (this.account.WithdrawMoney(amountToWithdraw) == 0))
                {
                    do
                    {
                        try
                        {
                            Console.WriteLine("\nYou cannot withdraw more money than your balance!");
                            Console.Write("\n\nEnter the amount of money you want to withdraw here: ");
                            amountToWithdraw = int.Parse(Console.ReadLine());
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("\n\n" + exception.GetType().Name + ": " + exception.Message);
                            Console.WriteLine("Enter a proper integer that represents the amount of money you want to withdraw!\n\n");
                            continue;
                        }
                    } while (amountToWithdraw == 0);

                    this.account.WithdrawMoney(amountToWithdraw);
                }

                Console.Clear();
                Console.WriteLine("The transaction has been completed successfully!");
                Console.WriteLine("Your current balance is now: {0}", this.account.PrintStatement());

                this.ExitOrReturn();
                return;
            }
            public void SaveBeforeClosing(string filename)
            {
                this.bankSystem.Save(filename);
                return;
            }
        }
        public static void Main(string[] args)
        {
            IBankSystem bankSystem = BankSystem.Load("../../../BasicBank-BLL/bin/debug/Bank System.txt");
            IBankUI bankUI = new BankUI(bankSystem);
            bankUI.InitiateBankProgram();
        }
    }
}
