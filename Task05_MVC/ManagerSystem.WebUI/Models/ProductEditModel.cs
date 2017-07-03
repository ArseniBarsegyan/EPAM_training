using System.ComponentModel.DataAnnotations;

namespace ManagerSystem.WebUI.Models
{
    public class ProductEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Name can't contain numbers")]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}