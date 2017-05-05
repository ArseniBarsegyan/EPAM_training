using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
