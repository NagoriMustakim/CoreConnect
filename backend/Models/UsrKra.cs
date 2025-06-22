using LinkwayAPI.Data;

namespace LinkwayAPI.Models;

public partial class UsrKra
{
    public int Kraid { get; set; }

    public Guid Kraguid { get; set; }

    public int UserId { get; set; }

    public string Kratitle { get; set; } = null!;

    public string Kradescription { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public virtual UsrUser User { get; set; } = null!;
}
