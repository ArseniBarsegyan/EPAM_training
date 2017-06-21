using ManagerSystem.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace ManagerSystem.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store)
            : base(store)
        {

        }
    }
}