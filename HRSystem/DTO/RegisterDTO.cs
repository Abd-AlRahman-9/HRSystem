using System.ComponentModel.DataAnnotations;

namespace HRSystem.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string FullName { get; set; }
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
