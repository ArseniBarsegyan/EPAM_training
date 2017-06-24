using System.Linq;
using ManagerSystem.BLL.DTO;
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

        public UserDto GetById(string id)
        {
            var user = UnitOfWork.UserManager.Users.FirstOrDefault(x => x.Id == id);
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.UserName
            };
            return userDto;
        }
    }
}