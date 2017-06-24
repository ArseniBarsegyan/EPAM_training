using System.Collections.Generic;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Infrastructure;

namespace ManagerSystem.BLL.Interfaces
{
    public interface IManagerService
    {
        IEnumerable<ManagerDto> GetAllManagersList();
        ManagerDto GetManagerById(int id);
        OperationDetails Create(ManagerDto managerDto);
        OperationDetails Edit(ManagerDto managerDto);
        OperationDetails Delete(int id);
    }
}