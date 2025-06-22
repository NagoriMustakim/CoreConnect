using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Account
{
    public class ForgotPasswordDTO
    {
        [EmailAddress]
        [DataType(DataType.Password)]
        public string Email { get; set; }
    }
}
