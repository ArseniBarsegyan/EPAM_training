using System.Collections.Generic;

namespace ManagerSystem.DAL.Entities
{
    public class Manager : Entity
    {
        public Manager()
        {
            Orders = new List<Order>();
        }

        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}