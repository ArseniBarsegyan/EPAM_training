using System.ComponentModel.DataAnnotations;

namespace ManagerSystem.WebUI.Models
{
    public class ManagerCreateModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Name can't contain numbers")]
        public string LastName { get; set; }
    }
}