using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerSystem.WebUI.Models
{
    public class OrderCreateModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Name can't contain numbers")]
        public string ClientName { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Name can't contain numbers")]
        public string ManagerName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
    }
}