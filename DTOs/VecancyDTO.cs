using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploymentApi.DTOs
{
    public class VecancyDTO
    {
        [Required]
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        [Required]
        public string JobType { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int MaxApplicant { get; set; }
        [Required]
        public string Id { get; set; }
    }
}
