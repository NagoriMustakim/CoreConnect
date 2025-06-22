namespace LinkwayAPI.DTOs.GiftProgram
{
    public class GiftProgramDisplayDTO
    {
        public Guid GiftformGuid { get; set; }

        public string CandidateName { get; set; }

        public string CandidateGuid { get; set; }

        public string GiftformReviewerId { get; set; }

        public string GiftformReviewerName { get; set; }

        public Guid CurrentDesignationGuid { get; set; }

        public string CurrentDesignation { get; set; } = null!;

        public Guid DesiredDesignationGuid { get; set; }

        public string DesiredDesignation { get; set; } = null!;

        public string? GapsIdentified { get; set; }

        public string? PlanOfAction { get; set; }

        public DateTime? TargetAchievementDate { get; set; }

        public bool IsSupportRequired { get; set; }

        public string? EndResult { get; set; }

        public string? MajorAchievements { get; set; }

        public string LearningAndTransistionProcess { get; set; } = null!;

        public string CareerGrowthContribution { get; set; } = null!;

        public string? WorkRelatedTrainingAndCertifications { get; set; }

        public int GiftformManagerStatus { get; set; }

        public int GiftformAdminStatus { get; set; }

        public string? GiftformRejectionReason { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

    }
}
