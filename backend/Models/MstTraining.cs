using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class MstTraining
{
    public int TrainingId { get; set; }

    public Guid TrainingGuid { get; set; }

    public string TrainingTitle { get; set; } = null!;

    public string? TrainingDescription { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual ICollection<UsrTraining> UsrTrainings { get; set; } = new List<UsrTraining>();
}
