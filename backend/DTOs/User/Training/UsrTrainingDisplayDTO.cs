namespace LinkwayAPI.DTOs.User.Training
{
    public class UsrTrainingDisplayDTO
    {
        public Guid UserTrainingGuid { get; set; }
        public Guid TrainingTypeGuid { get; set; }
        public Guid TrainingNameGuid { get; set; }

        public string TrainingType { get; set; } = null!;

        public string TrainingTitle { get; set; }

        public DateTime TrainingStartDate { get; set; }

        public DateTime? TrainingEndDate { get; set; }

        public bool IsTrainingActive { get; set; }

        public string? UserTrainingDescription { get; set; }
    }
}
