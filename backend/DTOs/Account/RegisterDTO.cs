using LinkwayAPI.Constants.API;
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Account
{
    public class RegisterDTO
    {
        [Required]
        public int Role { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(ValidationConstant.ALPHABETS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [RegularExpression(ValidationConstant.ALPHABETS_REGEX, ErrorMessage = ValidationConstant.NAME_REGEX_MESSAGE)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Compare(AdminConstant.CONFIRM_PASSWORD)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [Required]
        public Guid DesignationGuid { get; set; }   
    }
}
