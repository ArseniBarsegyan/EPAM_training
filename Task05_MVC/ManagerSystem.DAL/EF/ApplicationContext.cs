using System.Data.Entity;
using ManagerSystem.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ManagerSystem.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
        }

        public ApplicationContext(string connectionString)
            : base(connectionString)
        {

        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}