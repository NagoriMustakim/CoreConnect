namespace LinkwayAPI.DTOs.InternalPrograms
{
    public class InternalProgramViewDTO
    {
        public Guid InternalProgramGuid { get; set; }

        public string InternalProgramName { get; set; }

        public string? InternalProgramDescription { get; set; }

        public int InternalProgramReviewCycle { get; set; }

        public int InternalProgramActiveDays { get; set; }

        public bool IsInternalProgramCategoryExists { get; set; }
        public bool IsActive { get; set; } = false;

        public DateTime InternalProgramStartDate { get; set; }

        public DateTime InternalProgramEndDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
