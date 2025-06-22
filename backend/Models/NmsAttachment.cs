using System;
using System.Collections.Generic;

namespace LinkwayAPI.Models;

public partial class NmsAttachment
{
    public int AttachmentId { get; set; }

    public Guid AttachmentGuid { get; set; }

    public int NominationId { get; set; }

    public string AttachmentName { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual NmsNomination Nomination { get; set; } = null!;
}
