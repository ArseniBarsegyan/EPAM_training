using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerSystem.DAL.Entities;
using Task04.DAL.Repositories;

namespace ManagerSystem.Service.Classes
{
    public class CsvDbRecorder
    {
        private string _nameOrConnectionString;
        private object _lockObject = new object();
        private GenericRepository<Manager> _managerRepo;

        public CsvDbRecorder(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }
    }
}
