using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    //Creating a document model from text file
    public class DocumentCreator
    {
        private StreamReader _streamReader;
        private List<Page> _pages;
        private Document _document;
        private Page _page;
        private string _fileName;
        private int _numberOfLinesPerPage;
        private string _exceptionMessage;

        public DocumentCreator(string fileName, int numberOfLinesPerPage)
        {
            
        }

    }
}
