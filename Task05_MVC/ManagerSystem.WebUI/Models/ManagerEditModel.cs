using System.ComponentModel.DataAnnotations;

namespace ManagerSystem.WebUI.Models
{
    public class ManagerEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Name can't contain numbers")]
        public string LastName { get; set; }
    }
}