using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    class User
    {
        private string username;
        private static Dictionary<string, Dictionary<string, dynamic>> users = new Dictionary<string, Dictionary<string, dynamic>>();
       
        public User(string username)
        {
            this.username = username;
            users.Add(username, new Dictionary<string, dynamic>
            {
                {"owedFromBank", 0m },
                {"transactions", new List<string>() }
            } );
        }

        public static bool userExists(string username)
        {
            return users.ContainsKey(username) ? true : false;
        }

        public void accountTransaction(decimal transactionAmount)
        {
            users[this.username]["owedFromBank"] += transactionAmount;
        }

        public void addTransactionDetails(string transaction)
        {
            users[this.username]["transactions"].Add(transaction);
        }

        public static void getOwedFromBank()
        {
            foreach (var user in users)
            {
                Console.WriteLine("{0}: {1}", user.Key, users[user.Key]["owedFromBank"]);
            }
        }

        public static void getTransactions()
        {
            foreach (var user in users)
            {
                foreach (string transaction in users[user.Key]["transactions"])
                {
                    Console.WriteLine("{0}", transaction);
                }
            }
        }

    }
}
