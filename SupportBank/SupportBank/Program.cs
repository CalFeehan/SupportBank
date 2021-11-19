using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SupportBank
{
    class Program
    {
        private static string path = @".\Transactions2014.csv";
        private static string[] text = Utility.readFileSkipLines(path, 1);
        // [transactionDate, debtor, creditor, service, amount]
        private static Dictionary<string, Account> users = new Dictionary<string, Account>();

        static void Main(string[] args)
        {
            initialiseDatabase();

            bool loop = true;
            while (loop) {
                string userInput = Utility.userInput();
                switch (userInput.ToLower())
                {
                    case "quit":
                        loop = false;
                        break;
                    case "all":
                        printAllBalances();
                        break;
                    default:
                        try
                        {
                            printAllTransactions(users[userInput]);
                        }
                        catch 
                        { 
                            // log no account exists
                        }
                        break;
                }
            }
        }

        static void initialiseDatabase()
        {
            foreach (string line in text)
            {
                string[] item = line.Split(",");

                if (!DateTime.TryParse(item[0], out DateTime transactionDate))
                {
                    // log error
                    continue;
                }
                string debtor = item[1];
                string creditor = item[2];
                string service = item[3];
                if (!decimal.TryParse(item[4], out decimal amount))
                {
                    // log error
                    continue;
                }

                users.TryAdd(creditor, new Account(creditor));
                users.TryAdd(debtor, new Account(debtor));

                Transaction transaction = new Transaction(transactionDate, users[debtor], users[creditor], service, amount);

                users[creditor].updateBalance(transaction);
                users[debtor].updateBalance(transaction);
            }
        }

        static void printAllBalances()
        {
            foreach (var pair in users)
            {
                Console.WriteLine($"{pair.Value.Username}: {pair.Value.Balance:C}");
            }
        }

        static void printAllTransactions(Account account)
        {
            Console.WriteLine(account.getTransactions());
        }
    }
}