using LinkwayAPI.DTOs.Attachment;
using LinkwayAPI.Models;

namespace LinkwayAPI.DTOs.Nomination
{
    public class NominationDisplayDTO
    {
        public Guid NominationGuid { get; set; }

        public string NominatorName { get; set; }

        public string NomineeName { get; set; }

        public string InternalProgramTitle { get; set; }

        public string InternalProgramCategory { get; set; } = null!;

        public int NominationStatus { get; set; }

        public int? CycleIteration { get; set; }

        public string JustificationComment { get; set; } = null!;

        public string? NominationRejectionReason { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public virtual ICollection<AttachmentDisplayDTO> NmsAttachments { get; set; } 

    }
}
