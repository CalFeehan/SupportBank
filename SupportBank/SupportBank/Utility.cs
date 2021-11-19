using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SupportBank
{
    class Utility
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static string[] readFileSkipLines(string path, int linesToSkip)
        {
            Logger.Info($"Function readFileSkipLines called with path {path} and linesToSkip {linesToSkip}");

            try
            {
                string[] text = File.ReadAllLines(path);
                text = text.Skip(linesToSkip).ToArray();
                return text;
            }
            catch (FileNotFoundException e) 
            {
                Logger.Info($"{e} thrown on file {path}");
                throw;
            }
        }

        public static string userInput()
        {
            while (true)
            {
                Console.WriteLine("Type 'List All' for a list of all accounts and balances \nType 'List [account name]' to see transactions for that account \nOr type 'quit' to exit");
                string userInput = Console.ReadLine();
                Regex rx = new Regex(@"(?i)List +([\w ]+)|(quit)");
                Match rxMatch = rx.Match(userInput);

                if (rxMatch.Success)
                {
                    if (rxMatch.Groups[1].Success) { return rxMatch.Groups[1].Value; }
                    else { return rxMatch.Groups[2].Value; }
                }
                else
                {
                    Logger.Warn($"User input not allowed {userInput}");
                    continue;
                }
            }
            
        }
    }
}
