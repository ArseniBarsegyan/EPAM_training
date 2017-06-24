using System;

namespace ManagerSystem.BLL.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string ManagerName { get; set; }
        public DateTime Date { get; set; }
        public string ClientName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}