using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploymentApi.Models
{
    public class Employer
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string Company { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
