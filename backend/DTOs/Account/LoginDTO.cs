using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Account
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
