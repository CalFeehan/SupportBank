using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SupportBank
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static string path = @".\DodgyTransactions2015.csv";
        private static string[] text = Utility.readFileSkipLines(path, 1);

        private static Dictionary<string, Account> users = new Dictionary<string, Account>();

        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Users\Callum.Feehan\OneDrive\Documents\Apprenticeship\C Sharp Bootcamp\SupportBank\SupportBank\SupportBank\logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;

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
                        try { printAllTransactions(users[userInput]); }
                        catch
                        {
                            Logger.Warn($"No account found with name {userInput}");
                        }
                        break;
                }
            }
        }

        static void initialiseDatabase()
        {
            Logger.Info("Function initialiseDatabase called");
            int i = 1;
            foreach (string line in text)
            {
                string[] item = line.Split(",");

                if (!DateTime.TryParse(item[0], out DateTime transactionDate))
                {
                    Logger.Error($"Could not parse datetime from line {i}");
                    i++;
                    continue;
                }
                string debtor = item[1];
                string creditor = item[2];
                string service = item[3];
                if (!decimal.TryParse(item[4], out decimal amount))
                {
                    Logger.Error($"Could not parse decimal from line {i}");
                    i++;
                    continue;
                }

                users.TryAdd(creditor, new Account(creditor));
                users.TryAdd(debtor, new Account(debtor));

                Transaction transaction = new Transaction(transactionDate, users[debtor], users[creditor], service, amount);

                users[creditor].updateBalance(transaction);
                users[debtor].updateBalance(transaction);

                i++;
            }
        }

        static void printAllBalances()
        {
            Logger.Info("Function printAllBalances called");

            foreach (var pair in users)
            {
                Console.WriteLine($"{pair.Value.Username}: {pair.Value.Balance:C}");
            }
        }

        static void printAllTransactions(Account account)
        {
            Logger.Info($"Function printAllTransactions called on account {account.Username}.");

            Console.WriteLine(account.getTransactions());
        }
    }
}