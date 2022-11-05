using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AbcProjectManagement.Models
{
    public class EditProjectTeamViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
