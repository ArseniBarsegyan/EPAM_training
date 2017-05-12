using System;
using System.Collections.Generic;
using Parser;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "how to test my app? simple.";
            List<Sentence> sentences = text.ParseText();
            Document document = new Document(sentences);

            var sentences1 = document.ReplaceAllWordsBySubString(2, "replaced string!");
            foreach (var sentence in sentences1)
            {
                Console.WriteLine(sentence);
            }

            var textWithoutConsonantWords = document.RemoveAllWordsStartWithConsonant();
            foreach (var sentence in textWithoutConsonantWords)
            {
                Console.WriteLine(sentence);
            }
        }
    }
}
