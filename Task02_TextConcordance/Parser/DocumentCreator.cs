using System.Collections.Generic;
using System.IO;

namespace Parser
{
    //Creating a document model from text file
    public class DocumentCreator
    {
        private StreamReader _streamReader;
        private ICollection<Page> _pages;
        private Page _page;
        private string _fileName;
        private int _numberOfLinesPerPage;

        public DocumentCreator(string fileName, int numberOfLinesPerPage)
        {
            _fileName = fileName;
            _numberOfLinesPerPage = numberOfLinesPerPage;
        }

        //Reading file data and filling pages
        //Every page can contain only limited number of lines
        private void ReadFileContent()
        {
            _streamReader = new StreamReader(_fileName);
            _pages = new List<Page>();

            string line;
            int countLines = 1;
            int pageNumber = 1;
            _page = new Page(pageNumber, _numberOfLinesPerPage, new List<string>());

            while((line = _streamReader.ReadLine()) != null)
            {
                //When all lines of page are filled adding filled page to list of pages
                //Creating new page and start filling it again. Counting of lines starting again
                if (countLines > _numberOfLinesPerPage)
                {
                    _pages.Add(_page);
                    pageNumber++;
                    _page = new Page(pageNumber, _numberOfLinesPerPage, new List<string>());
                    countLines = 1;
                }

                //if page isn't filled continue to filling it and counting lines
                _page.AddLine(line);
                countLines++;
            }

            //Adding last page to page list
            _pages.Add(_page);
            _streamReader.Close();
        }

        /// <summary>
        /// Creating document model if file exists.
        /// </summary>
        /// <returns>Document</returns>
        /// <exception cref="FileNotFoundException" />
        public Document CreateDocumentModel()
        {
            if (!File.Exists(_fileName)) throw new FileNotFoundException();
            ReadFileContent();
            return new Document(_fileName, _pages);
        }
    }
}
