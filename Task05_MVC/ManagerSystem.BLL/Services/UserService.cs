using ManagerSystem.BLL.Interfaces;
using ManagerSystem.DAL.Interfaces;

namespace ManagerSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork UnitOfWork { get; }

        public UserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}