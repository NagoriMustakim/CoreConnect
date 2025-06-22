namespace LinkwayAPI.DTOs.InternalProgramsCategories
{
    public class InternalProgramCategoryModifyDTO
    {

        public Guid InternalProgramCategoryGuid { get; set; }

        public Guid InternalProgramGuid { get; set; }

        public string InternalProgramCategory { get; set; } = null!;

        public string InternalProgramCategoryDescription { get; set; } = null!;
    }
}
