using System.ComponentModel.DataAnnotations;

namespace EmploymentApi.DTOs
{
    public class RegisterDTO
    {
        [StringLength(128)]
        public string Username { get; set; }

        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Password { get; set; }
        public bool IsEmployer { get; set; } = false;  
    }
}
