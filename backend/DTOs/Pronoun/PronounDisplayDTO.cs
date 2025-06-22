namespace LinkwayAPI.DTOs.Pronoun
{
    public class PronounDisplayDTO
    {
        public Guid PronounGuid { get; set; }

        public string Pronoun { get; set; }

        public string? PronounDescription { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
