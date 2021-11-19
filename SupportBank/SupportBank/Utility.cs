using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SupportBank
{
    class Utility
    {

        public static string[] readFileSkipLines(string path, int linesToSkip)
        {
            string[] text = File.ReadAllLines(path);
            text = text.Skip(linesToSkip).ToArray();
            return text;
        }
        
    }
}
