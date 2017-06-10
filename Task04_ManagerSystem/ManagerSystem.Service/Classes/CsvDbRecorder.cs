using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ManagerSystem.DAL.EF;
using ManagerSystem.DAL.Entities;
using ManagerSystem.Service.Models;
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

        public void WriteDataToDataBaseFromFile(string fileName)
        {
            _managerRepo = new GenericRepository<Manager>(new AppDbContext(_nameOrConnectionString));

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            if (fileNameWithoutExtension == null) return;
            var splittedFileName = fileNameWithoutExtension.Split('_');
            var managerName = splittedFileName[0];
            var managerDate = splittedFileName[1];

            var purchaseDto = new PurchaseDto()
            {
                ManagerName = managerName,
                Date = DateTime.ParseExact(managerDate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture),
            };

            using (var reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var lineContent = line.Split(',');
                    purchaseDto.ClientName = lineContent[0].Trim();
                    purchaseDto.ProductName = lineContent[1].Trim();
                    purchaseDto.Price = decimal.Parse(lineContent[2].Trim());

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PurchaseDto, Product>()
                            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.ProductName))
                            .ForMember(x => x.Date, opt => opt.MapFrom(src => src.Date))
                            .ForMember(x => x.Client, opt => opt.MapFrom(src => src.ClientName))
                            .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Price));
                        cfg.CreateMap<PurchaseDto, Manager>()
                            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.ManagerName));
                    });

                    var product = Mapper.Map<PurchaseDto, Product>(purchaseDto);
                }
            }
        }
    }
}
