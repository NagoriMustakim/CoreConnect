namespace LinkwayAPI.DTOs.InternalProgramsCategories
{
    public class InternalProgramCategoryCreateDTO
    {

        public Guid InternalProgramGuid { get; set; }

        public string InternalProgramCategory { get; set; } = null!;

        public string InternalProgramCategoryDescription { get; set; } = null!;
    }
}
