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
            List<Sentence> sentences = new List<Sentence>();
            List<Symbol> symbols = new List<Symbol>();
            List<ISentenceItem> sentenceItems = new List<ISentenceItem>();

            ReplaceAllTabsAndWhiteSpaceSequences(text);

            for (int i = 0; i < text.Length - 1; i++)
            {
                if (IsSymbol(text[i].ToString()))
                {
                    symbols.Add(new Symbol(text[i]));
                }
                else if (IsWordSeparator(text[i].ToString()))
                {
                    if (symbols.Count != 0)
                    {
                        sentenceItems.Add(new Word(symbols));
                    }
                    symbols = new List<Symbol>();
                    sentenceItems.Add(new WordSeparator(text[i].ToString()));
                }
                else if (IsSentenceSeparator(text[i].ToString()))
                {
                    if (symbols.Count != 0)
                    {
                        sentenceItems.Add(new Word(symbols));
                    }
                    symbols = new List<Symbol>();

                    sentenceItems.Add(new SentenceSeparator(text[i].ToString()));
                    if (!IsSentenceSeparator(text[i + 1].ToString()))
                    {
                        sentences.Add(new Sentence(sentenceItems));
                        sentenceItems = new List<ISentenceItem>();
                    }
                }
            }
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
