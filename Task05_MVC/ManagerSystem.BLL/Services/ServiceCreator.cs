using ManagerSystem.BLL.Interfaces;
using ManagerSystem.DAL.Repositories;

namespace ManagerSystem.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connectionString)
        {
            return new UserService(new UnitOfWork(connectionString));
        }
    }
}