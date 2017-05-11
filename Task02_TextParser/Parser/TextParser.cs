using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser
{
    public static class TextParser
    {
        public static List<Sentence> ParseText(this string text)
        {
            
        }

        private static void ReplaceAllTabsAndWhiteSpaceSequences(string text)
        {
            text = Regex.Replace(text, @"\s+", " ");
            text = Regex.Replace(text, @"\t+", " ");
        }

        private static bool IsSymbol(string s)
        {
            return new Regex("[A-Za-z0-9]").IsMatch(s);
        }

        private static bool IsWordSeparator(string s)
        {
            return new Regex(@"\s|\:|\-|\'|\,").IsMatch(s);
        }

        private static bool IsSentenceSeparator(string s)
        {
            return new Regex(@"\.|\?|\!").IsMatch(s);
        }
    }
}
