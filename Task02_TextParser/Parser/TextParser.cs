using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parser
{
    public static class TextParser
    {
        public static ICollection<Sentence> ParseText(this string text)
        {
            var sentences = new List<Sentence>();
            var symbols = new List<Symbol>();
            var sentenceItems = new List<ISentenceItem>();

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

            if (symbols.Count != 0)
            {
                sentenceItems.Add(new Word(symbols));
            }
            symbols = new List<Symbol>();
            sentenceItems.Add(new SentenceSeparator(text[text.Length - 1].ToString()));
            sentences.Add(new Sentence(sentenceItems));
            
            RemoveAllFirstWhiteSpacesInSentences(sentences);
            return sentences;
        }

        private static void RemoveAllFirstWhiteSpacesInSentences(ICollection<Sentence> sentences)
        {
            foreach (var item in sentences)
            {
                if (item.SentenceItems.ElementAt(0) is WordSeparator)
                {
                    item.SentenceItems.Remove(item.SentenceItems.ElementAt(0));
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
