using Parser;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentCreator creator = new DocumentCreator("D:\\TestDocumentForTask2.txt", 5);
            Document document = creator.CreateDocumentModel();

            TextParser parser = new TextParser(document);
            parser.FillInWordsList();
            Concordance concordance = parser.CreateConcordance();
            string sortedWords = concordance.GroupByAlphaBet();
            Console.WriteLine(sortedWords);
        }
    }
}
