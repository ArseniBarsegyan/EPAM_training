using System;
using System.Threading.Tasks;
using ManagerSystem.DAL.EF;
using ManagerSystem.DAL.Entities;
using ManagerSystem.DAL.Identity;
using ManagerSystem.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ManagerSystem.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _context = new ApplicationContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
            ClientRepository = new GenericRepository<Client>(_context);
            ProductRepository = new GenericRepository<Product>(_context);
            ManagerRepository = new GenericRepository<Manager>(_context);
            OrderRepository = new GenericRepository<Order>(_context);
        }

        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public GenericRepository<Client> ClientRepository { get; }
        public GenericRepository<Product> ProductRepository { get; }
        public GenericRepository<Manager> ManagerRepository { get; }
        public GenericRepository<Order> OrderRepository { get; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                UserManager.Dispose();
                RoleManager.Dispose();
            }
            _disposed = true;
        }
    }
}