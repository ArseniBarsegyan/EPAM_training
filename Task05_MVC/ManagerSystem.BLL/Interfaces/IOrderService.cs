using System.Collections.Generic;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Infrastructure;

namespace ManagerSystem.BLL.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetAllOrderList();
        OrderDto GetOrderDtoById(int id);
        OperationDetails Create(OrderDto orderDto);
        OperationDetails Edit(OrderDto orderDto);
        OperationDetails Delete(int id);
    }
}