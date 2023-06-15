using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploymentApi.Models
{
    public class Vacancy
    {
        [Key]
        public int VacancyId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int MaxApplicant { get; set; }
        public bool IsArchived { get; set; } = false;
        public int NoOfApplied { get; set; }

        [ForeignKey("Employer")]
        public string Id { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
