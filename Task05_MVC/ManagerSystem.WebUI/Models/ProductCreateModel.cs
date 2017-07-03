using System.ComponentModel.DataAnnotations;

namespace ManagerSystem.WebUI.Models
{
    public class ProductCreateModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}