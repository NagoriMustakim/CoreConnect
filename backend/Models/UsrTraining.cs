using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrTraining
{
    public int UserTrainingId { get; set; }

    public Guid UserTrainingGuid { get; set; }

    public int TrainingTypeId { get; set; }

    public int UserId { get; set; }

    public int TrainingId { get; set; }

    public DateTime TrainingStartDate { get; set; }

    public DateTime? TrainingEndDate { get; set; }

    public bool IsTrainingActive { get; set; }

    public string? UserTrainingDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual MstTraining Training { get; set; } = null!;

    public virtual MstTrainingType TrainingType { get; set; } = null!;

    public virtual UsrUser User { get; set; } = null!;
}
