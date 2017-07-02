using System.Collections.Generic;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Infrastructure;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.DAL.Entities;
using ManagerSystem.DAL.Interfaces;

namespace ManagerSystem.BLL.Services
{
    public class ManagerService : IManagerService
    {
        private IUnitOfWork UnitOfWork { get; }

        public ManagerService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IEnumerable<ManagerDto> GetAllManagersList()
        {
            var managerList = new List<ManagerDto>();

            foreach (var manager in UnitOfWork.ManagerRepository.GetAll())
            {
                var managerDto = new ManagerDto
                {
                    Id = manager.Id,
                    LastName = manager.LastName
                };
                managerList.Add(managerDto);
            }
            return managerList;
        }

        public ManagerDto GetManagerById(int id)
        {
            var manager = UnitOfWork.ManagerRepository.GetById(id);
            var managerDto = new ManagerDto
            {
                Id = manager.Id,
                LastName = manager.LastName
            };
            return managerDto;
        }

        public OperationDetails Create(ManagerDto managerDto)
        {
            var manager = new Manager { LastName = managerDto.LastName };
            UnitOfWork.ManagerRepository.Create(manager);
            UnitOfWork.Save();

            return new OperationDetails(true, "manager create successful", "");
        }

        public OperationDetails Edit(ManagerDto managerDto)
        {
            var manager = UnitOfWork.ManagerRepository.GetById(managerDto.Id);
            manager.LastName = managerDto.LastName;
            UnitOfWork.ManagerRepository.Update(manager);
            UnitOfWork.Save();

            return new OperationDetails(true, "manager update successful", "");
        }

        public OperationDetails Delete(int id)
        {
            UnitOfWork.ManagerRepository.Delete(id);
            UnitOfWork.Save();

            return new OperationDetails(true, "manager delete successful", "");
        }
    }
}