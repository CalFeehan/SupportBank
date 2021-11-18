using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @".\Transactions2014.csv";
            string[] text = File.ReadAllLines(path);
            text = text.Skip(1).ToArray();

            Dictionary<string, Account> users = new Dictionary<string, Account>();

            // [transactionDate, debtor, creditor, service, amount]

            foreach (string line in text)
            {
                string[] item = line.Split(",");

                DateTime transactionDate = DateTime.Parse(item[0]);
                string debtor = item[1];
                string creditor = item[2];
                string service = item[3];
                decimal amount = Decimal.Parse(item[4]);

                users.TryAdd(creditor, new Account(creditor));
                users.TryAdd(debtor, new Account(debtor));

                Transaction transaction = new Transaction(transactionDate, users[debtor], users[creditor], service, amount);

                users[creditor].updateBalance(transaction);
                users[debtor].updateBalance(transaction);

                printAllTransactions(users[debtor]);

            }

            void printAllBalances()
            {
                foreach (var pair in users)
                {
                    Console.WriteLine($"{pair.Value.Username}: {pair.Value.Balance}");
                }
            }

            void printAllTransactions(Account account)
            {
                Console.WriteLine(account.ToString());
            }
        }
    }
}