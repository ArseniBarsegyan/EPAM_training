using System;
using System.Threading.Tasks;
using ManagerSystem.DAL.Entities;
using ManagerSystem.DAL.Identity;
using ManagerSystem.DAL.Repositories;

namespace ManagerSystem.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        GenericRepository<Client> ClientRepository { get; }
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<Manager> ManagerRepository { get; }
        GenericRepository<Order> OrderRepository { get; }
        Task SaveAsync();
        void Save();
    }
}