using System.Collections.Generic;
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

        public IEnumerable<UserDto> GetAllUsersList()
        {
            var userList = UnitOfWork.UserManager.Users.ToList();

            var userDtoList = userList.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.UserName,
                Role = user.Roles.First().RoleId
            }).ToList();
            return userDtoList.AsEnumerable();
        }
    }
}