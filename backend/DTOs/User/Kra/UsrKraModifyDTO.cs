using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.User.Kra
{
    public class UsrKraModifyDTO
    {
        [Required]
        public Guid Kraguid { get; set; }

        [Required]
        [MaxLength(100)]
        public string Kratitle { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string Kradescription { get; set; } = null!;
    }
}
