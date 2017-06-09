using System;

namespace ManagerSystem.Service.Models
{
    public class PurchaseDto
    {
        public string ManagerName { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public string ClientName { get; set; }
        public decimal Price { get; set; }
    }
}
