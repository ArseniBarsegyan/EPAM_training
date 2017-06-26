using System.Web.Mvc;
using ManagerSystem.BLL.DTO;
using PagedList;

namespace ManagerSystem.WebUI.Models
{
    public class OrderListViewModel
    {
        public IPagedList<OrderDto> Orders { get; set; }
        public SelectList Managers { get; set; }
        public SelectList Products { get; set; }
        public SelectList Dates { get; set; }
        public decimal FromValue { get; set; }
        public decimal ToValue { get; set; }
    }
}