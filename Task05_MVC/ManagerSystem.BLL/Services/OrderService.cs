using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.DAL.Repositories;

namespace ManagerSystem.BLL.Services
{
    public class OrderService : IOrderService
    {
        private UnitOfWork UnitOfWork { get; }

        public OrderService(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public OrderDto GetOrderDtoById(int id)
        {
            return GetAllOrderList().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<OrderDto> GetAllOrderList()
        {
            var orderList = new List<OrderDto>();
            var allOrders = UnitOfWork.OrderRepository.GetAll().Include(x => x.Client)
                .Include(x => x.Manager).Include(x => x.Product);

            foreach (var order in allOrders)
            {
                var orderDto = new OrderDto()
                {
                    Id = order.Id,
                    ClientName = order.Client.Name,
                    ManagerName = order.Manager.LastName,
                    ProductName = order.Product.Name,
                    Date = order.Date,
                    Price = order.Product.Price
                };
                orderList.Add(orderDto);
            }
            return orderList;
        }
    }
}