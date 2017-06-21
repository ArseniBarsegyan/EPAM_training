using ManagerSystem.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace ManagerSystem.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {

        }
    }
}