namespace LinkwayAPI.DTOs.Training
{
    public class TrainingListDTO
    {
        public Guid? TrainingGuid { get; set; }

        public string TrainingTitle { get; set; } = null!;

        public string? TrainingDescription { get; set; }
    }
}
