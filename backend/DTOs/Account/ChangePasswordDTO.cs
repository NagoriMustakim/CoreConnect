using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Account
{
    public class ChangePasswordDTO
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string NewPassword { get; set; }
    }
}
