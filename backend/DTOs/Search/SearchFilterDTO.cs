
using System.ComponentModel.DataAnnotations;

namespace LinkwayAPI.DTOs.Search
{
    public class SearchFilterDTO
    {
        [MaxLength(100)]
        public string? Skills { get; set; }
        public int? Experience { get; set; }
        [MaxLength(100)]
        public string? Projects { get; set; }
        [MaxLength(100)]
        public string? Certificates { get; set; }
        [MaxLength(100)]
        public string? Designation { get; set; }
        [MaxLength(100)]
        public string? Training { get; set; }

    }
}
