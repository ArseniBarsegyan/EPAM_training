using System;

namespace ManagerSystem.DAL.Entities
{
    public class Order : Entity
    {
        public DateTime Date { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}