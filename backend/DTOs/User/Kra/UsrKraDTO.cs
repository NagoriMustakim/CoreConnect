using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.User.Kra
{
    public class UsrKraDTO
    {
        [Required]
        [MaxLength(100)]
        public string Kratitle { get; set; }

        [Required]
        [MaxLength(500)]
        public string Kradescription { get; set; } = null!;
    }
}
