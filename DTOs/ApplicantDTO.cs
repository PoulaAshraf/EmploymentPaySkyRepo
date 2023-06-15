using System.ComponentModel.DataAnnotations;

namespace EmploymentApi.DTOs
{
    public class ApplicantDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Qualification { get; set; }
    }
}
