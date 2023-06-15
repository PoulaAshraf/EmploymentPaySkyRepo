namespace EmploymentApi.DTOs
{
    public class AppliedVacancyDTO
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }
        public int NoOfApplied { get; set; }
        public List<ApplicantDTO> Applicants { get; set; }
    }
}
