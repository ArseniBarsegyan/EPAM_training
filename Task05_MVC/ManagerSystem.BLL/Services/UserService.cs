using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerSystem.BLL.DTO;
using ManagerSystem.BLL.Infrastructure;
using ManagerSystem.BLL.Interfaces;
using ManagerSystem.DAL.Entities;
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

        public async Task<OperationDetails> CreateAsync(UserDto userDto)
        {
            var user = await UnitOfWork.UserManager.FindByNameAsync(userDto.Name);
            if (user != null) return new OperationDetails(false, "User with this login already exists", "Name");

            user = new ApplicationUser { UserName = userDto.Name };
            var result = await UnitOfWork.UserManager.CreateAsync(user, userDto.Password);

            if (result.Errors.Any())
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

            await UnitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await UnitOfWork.SaveAsync();
            return new OperationDetails(true, "register successfull", "");
        }
    }
}