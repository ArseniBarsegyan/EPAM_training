using System;
using System.Collections.Generic;
using System.Linq;
using Parser;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Text textModel = CreateTextModel("how     to test my app?     too simple?");
            ShowAllWordsInInterrogativeSentencesByLength(textModel, 3);
            ShowSplitterBetweenMethods();
            ShowSentencesSortedByWordsCount(textModel);
            ShowSplitterBetweenMethods();
            ReplaceAllWordsInSelectedSentenceWithSubStringByLength(textModel, 0, 4, "REPLACED STRING");
            ShowAllSentencesByOrder(textModel);
        }

        static Text CreateTextModel(string text)
        {
            return new Text(text.ParseText().ToList());
        }

        static void ShowAllSentencesByOrder(Text textModel)
        {
            Console.WriteLine("All text sentences by order: ");
            foreach (var sentence in textModel.Sentences)
            {
                Console.WriteLine(sentence);
            }
        }

        static void ShowSentencesSortedByWordsCount(Text textModel)
        {
            Console.WriteLine("All text sentences sorted by words count: ");
            foreach (var sentence in textModel.GetSortedSentencesByWordsCount())
            {
                Console.WriteLine(sentence);
            }
        }

        static void ShowAllWordsInInterrogativeSentencesByLength(Text textModel, int wordLength)
        {
            Console.WriteLine("All unique words with length {0} in interrogative sentences: ", wordLength);
            foreach (var word in textModel.GetUniqueWordsInInterrogativeSentences(wordLength))
            {
                Console.WriteLine(word);
            }
        }
        
        static void ReplaceAllWordsInSelectedSentenceWithSubStringByLength(Text textModel, int sentenceNumber, 
            int wordLength, string subString)
        {
            Console.WriteLine("Replaced string: {0}, replaced sentence: {1},  words length: {2}", 
                subString, sentenceNumber,wordLength);
            List<Sentence> subSentences = subString.ParseText().ToList();
            List<ISentenceItem> subStringItems = subSentences.ElementAt(0).SentenceItems.ToList();
            textModel.ReplaceAllWordsInSentenceBySubString(sentenceNumber, wordLength, subStringItems);
        }

        static void ShowSplitterBetweenMethods()
        {
            Console.WriteLine("----------");
        }
    }
}
