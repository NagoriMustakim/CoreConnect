using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstTrainingType
{
    public int TrainingTypeId { get; set; }

    public Guid TrainingTypeGuid { get; set; }

    public string TrainingType { get; set; } = null!;

    public string? TrainingTypeDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrTraining> UsrTrainings { get; set; } = new List<UsrTraining>();
}
