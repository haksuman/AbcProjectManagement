using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbcProjectManagement.Models
{
    public class ProjectsModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public string Team { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Range(0, 1)]
        public decimal Progress { get; set; }
    }
}
