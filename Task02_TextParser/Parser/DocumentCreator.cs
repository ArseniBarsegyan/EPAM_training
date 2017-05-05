﻿using System;
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
            _fileName = fileName;
            _numberOfLinesPerPage = numberOfLinesPerPage;
            try
            {
                _streamReader = new StreamReader(_fileName);
                ReadFileContent();
            }
            catch(FileNotFoundException e)
            {
                _exceptionMessage = e.Message;
            }
        }

        public string ExceptionMessage { get { return _exceptionMessage; } }

        //Reading file data and filling pages
        //Every page can contain only limited number of lines
        private void ReadFileContent()
        {
            _pages = new List<Page>();

            string line;
            int countLines = 1;
            int pageNumber = 1;
            _page = new Page(pageNumber, _numberOfLinesPerPage);

            while((line = _streamReader.ReadLine()) != null)
            {
                //When all lines of page are filled adding filled page to list of pages
                //Creating new page and start filling it again. Counting of lines starting again
                if (countLines > _numberOfLinesPerPage)
                {
                    _pages.Add(_page);
                    pageNumber++;
                    _page = new Page(pageNumber, _numberOfLinesPerPage);
                    countLines = 1;
                }

                //if page isn't filled continue to filling it and counting lines
                _page.AddLine(line);
                countLines++;
            }

            //Adding last page to page list
            _pages.Add(_page);

            //Closing stream
            _streamReader.Close();
        }

    }
}