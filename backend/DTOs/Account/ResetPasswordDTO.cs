using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Account
{
    public class ResetPasswordDTO
    {
        [DataType(DataType.Password)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
