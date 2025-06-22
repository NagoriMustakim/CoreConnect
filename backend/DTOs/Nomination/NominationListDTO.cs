namespace LinkwayAPI.DTOs.Nomination
{
    public class NominationListDTO
    {
        public Guid NominationId { get; set; }

        public string Nominator { get; set; }

        public string Nominee { get; set; }

        public string InternalProgram { get; set; }

        public string InternalProgramCategory { get; set; } = null!;

        public string JustificationComment { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

    }
}
