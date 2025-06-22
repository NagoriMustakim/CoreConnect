namespace LinkwayAPI.DTOs.GiftProgram
{
    public class GiftProgramStatusDTO
    {
        public int GiftformManagerStatus { get; set; }

        public int GiftformAdminStatus { get; set; }

        public string? GiftformRejectionReason { get; set; }

        public string GiftformReviewerId { get; set; }
    }
}
