using System.ComponentModel.DataAnnotations;

namespace AbcProjectManagement.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
