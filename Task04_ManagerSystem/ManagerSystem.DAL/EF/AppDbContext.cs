using System.Data.Entity;
using ManagerSystem.DAL.Entities;

namespace ManagerSystem.DAL.EF
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
        }

        public AppDbContext(string connectionString)
            : base(connectionString)
        {

        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}