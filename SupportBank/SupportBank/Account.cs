using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    class Account
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public string Username { get; }
        public decimal Balance { get; set; }
        private List<Transaction> Transactions { get; }

        public Account(string username)
        {
            Logger.Info($"Account created for {username}");
            Username = username;
            Balance = 0m;
            Transactions = new List<Transaction>();
        }

        public void updateBalance(Transaction transaction)
        {
            Logger.Info($"Function updateBalance called for {this.Username}");
            Balance = transaction.Debtor == this ? Balance - transaction.Amount : Balance + transaction.Amount;
            Transactions.Add(transaction);
        }

        public string getTransactions()
        {
            Logger.Info("Function getTransactions called");
            string output = "";
            foreach (Transaction transaction in Transactions)
            {
                output += transaction.ToString() + "\n";
            }
            return output;
        }
    }
}
