using System;
using System.Configuration;
using System.IO;
using Parser;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateTextConcordance();
        }

        static void CreateTextConcordance()
        {
            DocumentCreator creator = new DocumentCreator(ConfigurationManager.AppSettings["inputFileName"],
                Convert.ToInt32(ConfigurationManager.AppSettings["numberOfLinesPerPage"]));
            try
            {
                Document document = creator.CreateDocumentModel();
                Concordance concordance = new TextParser(document).CreateConcordance();
                concordance.WriteFile(ConfigurationManager.AppSettings["outputFileName"]);
                Console.WriteLine("Concordance have been created successfully");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Document doesn't exists. Please check your app settings");
            }
        }
    }
}
