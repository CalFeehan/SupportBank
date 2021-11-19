using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    class Account
    {
        public string Username { get; }
        public decimal Balance { get; set; }
        private List<Transaction> Transactions { get; }

        public Account(string username)
        {
            Username = username;
            Balance = 0m;
            Transactions = new List<Transaction>();
        }

        public void updateBalance(Transaction transaction)
        {
            Balance = transaction.Debtor == this ? Balance - transaction.Amount : Balance + transaction.Amount;
            Transactions.Add(transaction);
        }

        public string getTransactions()
        {
            string output = "";
            foreach (Transaction transaction in Transactions)
            {
                output += transaction.ToString() + "\n";
            }
            return output;
        }
    }
}
