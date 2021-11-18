using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    class Transaction
    {
        public DateTime TransactionDate { get; }
        public Account Debtor { get; }
        public Account Creditor { get; }
        public string Service { get; }
        public decimal Amount { get; }
        public string TransactionMessage { get; }

        public Transaction(DateTime transactionDate, Account debtor, Account creditor, string service, decimal amount)
        {
            TransactionDate = transactionDate;
            Debtor = debtor;
            Creditor = creditor;
            Service = service;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{TransactionDate.ToShortDateString()}: {Debtor.Username} paid {Creditor.Username} {Amount:C} for {Service}";
        }
    }
}
