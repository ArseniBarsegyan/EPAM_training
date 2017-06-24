using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.DAL.Repositories;
using AutoMapper;
using ManagerSystem.BLL.Infrastructure;
using ManagerSystem.DAL.Entities;

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

        public OperationDetails Create(OrderDto orderDto)
        {
            InitializeMapper();
            var order = Mapper.Map<OrderDto, Order>(orderDto);
            var manager = UnitOfWork.ManagerRepository.GetAll().FirstOrDefault(x => x.LastName == orderDto.ManagerName);
            var product = UnitOfWork.ProductRepository.GetAll().FirstOrDefault(x => x.Name == orderDto.ProductName);
            var client = UnitOfWork.ClientRepository.GetAll().FirstOrDefault(x => x.Name == orderDto.ClientName);

            if (product == null)
            {
                product = Mapper.Map<OrderDto, Product>(orderDto);
                UnitOfWork.ProductRepository.Create(product);
            }
            if (client == null)
            {
                client = Mapper.Map<OrderDto, Client>(orderDto);
                UnitOfWork.ClientRepository.Create(client);
            }
            order.Product = product;
            order.Client = client;

            if (manager == null)
            {
                manager = Mapper.Map<OrderDto, Manager>(orderDto);
                manager.Orders.Add(order);
                UnitOfWork.ManagerRepository.Create(manager);
            }
            else
            {
                manager.Orders.Add(order);
                UnitOfWork.ManagerRepository.Update(manager);
            }
            order.Manager = manager;
            UnitOfWork.Save();

            return new OperationDetails(true, "successfull create", "");
        }

        public OperationDetails Edit(OrderDto orderDto)
        {
            InitializeMapper();
            var order = UnitOfWork.OrderRepository.GetById(orderDto.Id);
            var manager = UnitOfWork.ManagerRepository.GetAll().FirstOrDefault(x => x.LastName == orderDto.ManagerName);
            var product = UnitOfWork.ProductRepository.GetAll().FirstOrDefault(x => x.Name == orderDto.ProductName);
            var client = UnitOfWork.ClientRepository.GetAll().FirstOrDefault(x => x.Name == orderDto.ClientName);

            if (manager == null)
            {
                manager = Mapper.Map<OrderDto, Manager>(orderDto);
                UnitOfWork.ManagerRepository.Create(manager);
            }
            if (product == null)
            {
                product = Mapper.Map<OrderDto, Product>(orderDto);
                UnitOfWork.ProductRepository.Create(product);
            }
            if (client == null)
            {
                client = Mapper.Map<OrderDto, Client>(orderDto);
                UnitOfWork.ClientRepository.Create(client);
            }
            order.Product = product;
            order.Client = client;
            order.Manager = manager;

            UnitOfWork.OrderRepository.Update(order);
            UnitOfWork.Save();

            return new OperationDetails(true, "successfull update", "");
        }

        public OperationDetails Delete(int id)
        {
            UnitOfWork.OrderRepository.Delete(id);
            UnitOfWork.Save(); ;

            return new OperationDetails(true, "successfull delete", "");
        }

        private void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<OrderDto, Product>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(src => src.ProductName))
                    .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Price));
                cfg.CreateMap<OrderDto, Manager>()
                    .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.ManagerName));
                cfg.CreateMap<OrderDto, Client>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(src => src.ClientName));
                cfg.CreateMap<OrderDto, Order>()
                    .ForMember(x => x.Date, opt => opt.MapFrom(src => src.Date));
            });
        }
    }
}