namespace LinkwayAPI.DTOs.InternalProgramsCategories
{
    public class InternalProgramCategoryViewDTO
    {
        public Guid InternalProgramCategoryGuid { get; set; }

        public Guid InternalProgramGuid { get; set; }

        public string InternalProgramName { get; set; } = null!;

        public string InternalProgramCategory { get; set; } = null!;

        public string InternalProgramCategoryDescription { get; set; } = null!;

    }
}
