namespace LinkwayAPI.DTOs.TrainingType
{
    public class TrainingTypeViewDTO
    {
        public Guid TrainingTypeGuid { get; set; }

        public string TrainingType { get; set; } = null!;

        public string? TrainingTypeDescription { get; set; }
    }
}
