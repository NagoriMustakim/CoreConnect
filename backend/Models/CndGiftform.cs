using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class CndGiftform
{
    public int GiftformId { get; set; }

    public Guid GiftformGuid { get; set; }

    public int CandidateId { get; set; }

    public int? GiftformReviewerId { get; set; }

    public int CurrentDesignationId { get; set; }

    public int DesiredDesignationId { get; set; }

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

    public virtual UsrUser Candidate { get; set; } = null!;

    public virtual MstDesignation CurrentDesignation { get; set; } = null!;

    public virtual MstDesignation DesiredDesignation { get; set; } = null!;

    public virtual UsrUser? GiftformReviewer { get; set; }
}
