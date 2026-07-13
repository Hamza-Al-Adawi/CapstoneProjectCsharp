namespace BankingSystemApp
{
    internal class Program
    
        /* NOTICE: this project was done with the help of AI tool */

    {
        // these 3 lists hold all the data, same index = same account
        static List<string> customerNames = new List<string>();
        static List<string> accountNumbers = new List<string>();
        static List<double> balances = new List<double>();

        static void Main(string[] args)
        {
            bool exitApp = false;

            while (!exitApp)
            {
                // show the menu every time the loop runs
                Console.WriteLine("\n===== Welcome to Spark Bank =====");
                Console.WriteLine("1. Add New Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Show Balance");
                Console.WriteLine("5. Transfer Amount");
                Console.WriteLine("6. List All Accounts");
                Console.WriteLine("7. Find Richest Customer");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");

                int choice;
                try
                {
                    // if user types something weird like "abc" this catch will save us
                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please enter a number from 1 to 8.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddAccount();
                        break;
                    case 2:
                        DepositMoney();
                        break;
                    case 3:
                        WithdrawMoney();
                        break;
                    case 4:
                        ShowBalance();
                        break;
                    case 5:
                        TransferAmount();
                        break;
                    case 6:
                        ListAllAccounts();
                        break;
                    case 7:
                        FindRichestCustomer();
                        break;
                    case 8:
                        exitApp = true;
                        Console.WriteLine("Thank you for banking with Spark Bank. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option, please choose between 1 and 8.");
                        break;
                }
            }
        }

        static void AddAccount()
        {
            // خدمة إضافة حساب جديد
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            Console.Write("Enter new account number: ");
            string accNum = Console.ReadLine();

            // check if this account number already exists
            if (accountNumbers.Contains(accNum))
            {
                Console.WriteLine("Error: this account number already exists.");
                return; // stop here, don't add anything
            }

            // get the starting balance
            Console.Write("Enter initial deposit amount: ");
            double startBalance;
            try
            {
                startBalance = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error: invalid amount entered.");
                return;
            }

            // can't open account with negative money
            if (startBalance < 0)
            {
                Console.WriteLine("Error: initial deposit can't be negative.");
                return;
            }

            // add to all 3 lists at the same time so they stay in sync
            customerNames.Add(name);
            accountNumbers.Add(accNum);
            balances.Add(startBalance);

            Console.WriteLine("Account created! Name: " + name + " | Account: " + accNum + " | Balance: " + startBalance);
        }

        static void DepositMoney()
        {
            Console.Write("Enter account number: ");
            string accNum = Console.ReadLine();

            // find which index this account is at
            int index = accountNumbers.IndexOf(accNum);

            // -1 means not found
            if (index == -1)
            {
                Console.WriteLine("Error: account not found.");
                return;
            }

            Console.Write("Enter deposit amount: ");
            double amount;
            try
            {
                amount = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error: invalid amount.");
                return;
            }

            // إيداع المبلغ يجب أن يكون موجباً
            if (amount <= 0)
            {
                Console.WriteLine("Error: deposit amount must be positive.");
                return;
            }

            // add the money
            balances[index] += amount;
            Console.WriteLine("Deposit successful! New balance for " + customerNames[index] + ": " + balances[index]);
        }

        static void WithdrawMoney()
        {
            Console.Write("Enter account number: ");
            string accNum = Console.ReadLine();

            int index = accountNumbers.IndexOf(accNum);

            if (index == -1)
            {
                Console.WriteLine("Error: account not found.");
                return;
            }

            Console.Write("Enter withdrawal amount: ");
            double amount;
            try
            {
                amount = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error: invalid amount.");
                return;
            }

            // can't withdraw zero or negative
            if (amount <= 0)
            {
                Console.WriteLine("Error: withdrawal amount must be positive.");
                return;
            }

            // make sure they have enough money
            if (amount > balances[index])
            {
                Console.WriteLine("Error: not enough balance. Current balance is: " + balances[index]);
                return;
            }

            // take the money out
            balances[index] -= amount;
            Console.WriteLine("Withdrawal successful! New balance for " + customerNames[index] + ": " + balances[index]);
        }

        static void ShowBalance()
        {
            // عرض رصيد الحساب
            Console.Write("Enter account number: ");
            string accNum = Console.ReadLine();

            int index = accountNumbers.IndexOf(accNum);

            if (index == -1)
            {
                Console.WriteLine("Error: account not found.");
                return;
            }

            // print all info about this account
            Console.WriteLine("Name: " + customerNames[index] + " | Account: " + accountNumbers[index] + " | Balance: " + balances[index]);
        }

        static void TransferAmount()
        {
            Console.Write("Enter sender account number: ");
            string senderAcc = Console.ReadLine();

            Console.Write("Enter receiver account number: ");
            string receiverAcc = Console.ReadLine();

            // find both indexes
            int senderIndex = accountNumbers.IndexOf(senderAcc);
            int receiverIndex = accountNumbers.IndexOf(receiverAcc);

            // check sender exists
            if (senderIndex == -1)
            {
                Console.WriteLine("Error: sender account not found.");
                return;
            }

            // check receiver exists
            if (receiverIndex == -1)
            {
                Console.WriteLine("Error: receiver account not found.");
                return;
            }

            Console.Write("Enter transfer amount: ");
            double amount;
            try
            {
                amount = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error: invalid amount.");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Error: transfer amount must be positive.");
                return;
            }

            // sender must have enough
            if (amount > balances[senderIndex])
            {
                Console.WriteLine("Error: sender doesn't have enough balance. Current balance: " + balances[senderIndex]);
                return;
            }

            // do the actual transfer
            balances[senderIndex] -= amount;
            balances[receiverIndex] += amount;

            // تأكيد التحويل
            Console.WriteLine("Transfer done!");
            Console.WriteLine(customerNames[senderIndex] + " new balance: " + balances[senderIndex]);
            Console.WriteLine(customerNames[receiverIndex] + " new balance: " + balances[receiverIndex]);
        }

        static void ListAllAccounts()
        {
            // عرض جميع الحسابات
            if (customerNames.Count == 0)
            {
                Console.WriteLine("No accounts exist yet.");
                return;
            }

            Console.WriteLine("All accounts:");
            // just loop through and print each one
            for (int i = 0; i < customerNames.Count; i++)
            {
                Console.WriteLine((i + 1) + ". Name: " + customerNames[i] + " | Account: " + accountNumbers[i] + " | Balance: " + balances[i]);
            }
        }

        static void FindRichestCustomer()
        {
            // find who has the most money
            if (balances.Count == 0)
            {
                Console.WriteLine("No accounts exist yet.");
                return;
            }

            // start by assuming index 0 is the richest
            int richestIndex = 0;
            double highestBalance = balances[0];

            for (int i = 1; i < balances.Count; i++)
            {
                // if we find someone with more money update the richest
                if (balances[i] > highestBalance)
                {
                    highestBalance = balances[i];
                    richestIndex = i;
                }
            }

            Console.WriteLine("Richest customer: " + customerNames[richestIndex] + " | Account: " + accountNumbers[richestIndex] + " | Balance: " + balances[richestIndex]);
        }
    }
}